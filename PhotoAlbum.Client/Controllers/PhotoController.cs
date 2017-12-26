using AutoMapper;
using Newtonsoft.Json;
using PhotoAlbum.Client.BusinessServices.Interfaces;
using PhotoAlbum.Client.BusinessServices.Services;
using PhotoAlbum.Client.Dto;
using PhotoAlbum.Client.Filters;
//using PhotoAlbum.Client.Model.Services;
using PhotoAlbum.Client.Models;
using PhotoAlbum.Common.Enums;
using PhotoAlbum.Common.ErrorCodes;
using PhotoAlbum.Common.Exceptions;
using PhotoAlbum.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace PhotoAlbum.Client.Controllers
{
    public class PhotoController : Controller
    {
        private readonly IPhotoAlbumService _photoAlbumService;

        public PhotoController(IPhotoAlbumService photoAlbumService)
        {
            _photoAlbumService = photoAlbumService;
        }
        
        public async Task<ActionResult> Index()
        {
            var model = new SortPhotoViewModel();

            return View(model);
        }

        public async Task<ActionResult> UserPage(string username)
        {
            var model = new UserPageViewModel(){ UserName = username};

            return View(model);
        }

        public async Task<ActionResult> UserPageManage(string username)
        {
            ViewBag.UserName = username;
            ViewBag.Manage = "manage";

            if (username != User.Identity.Name)
            {
                throw new UserIsNotAuthorizedException(ErrorCodes.UserIsNotAuthorized);
            }

            var model = new UserPageViewModel() { UserName = User.Identity.Name };

            return View(model);
        }

        [Authorize]
        public ActionResult CreatePhoto(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            var model = new CreatePhotoViewModel();

            return PartialView(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreatePhoto(CreatePhotoViewModel model, string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            var token = ((ClaimsPrincipal)HttpContext.User).FindFirst("AcessToken").Value;

            if (model.Image != null)
            {
                using (var binaryReader = new BinaryReader(model.Image.InputStream))
                {
                    Session["PhotoFileImageData"] = binaryReader.ReadBytes(model.Image.ContentLength);
                }
                Session["PhotoFileMimeType"] = model.Image.ContentType;
                Session["PhotoFileName"] = Path.GetFileName(model.Image.FileName);
            }

            if (model.Image == null && Session["PhotoFileImageData"] != null)
            {
                if (ModelState.ContainsKey("Image"))
                    ModelState["Image"].Errors.Clear();
            }
            
            if (ModelState.IsValid && Session["PhotoFileImageData"] != null )
            {
                byte[] imageData = (byte[])Session["PhotoFileImageData"];
                
                var createPhotoDto = new CreatePhotoDto()
                {
                    Image = imageData,
                    ImageMimeType = (string)Session["PhotoFileMimeType"],
                    Title = model.Title,
                    Description = model.Description
                };

                await _photoAlbumService.CreatePhotoAsync(createPhotoDto, token);
                
                Session["PhotoFileImageData"] = null;
                Session["PhotoFileMimeType"] = null;
                Session["PhotoFileName"] = null;

                // Return EmptyResult() for modal window
                return new EmptyResult();
            }
            
            return PartialView(model);
        }
        
        public async Task<ActionResult> GetImageById(int id)
        {
            var requestedETag = Request.Headers["If-None-Match"];

            string requestETagValue = null;
            if (requestedETag != null)
            {
                requestETagValue = requestedETag.Trim('W', '/', '"');
            }

            var image = await _photoAlbumService.GetImageByIdAsync(id, requestETagValue);

            if (image.IsNotModified)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotModified);
            }

            string unifiedData = JsonConvert.SerializeObject(image);
            var responseETagValue = ETagHashCreator.ComputeHash(unifiedData);
            if (requestETagValue == responseETagValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotModified);
            }
            
            var responseETag = new EntityTagHeaderValue("\"" + responseETagValue + "\"", true).ToString();

            Response.Cache.SetCacheability(HttpCacheability.ServerAndPrivate);
            Response.Cache.SetETag(responseETag);

            return File(image.Image, image.ImageMimeType);
        }

        [Authorize]
        public async Task<ActionResult> DeletePhotoById(int id)
        {
            var token = ((ClaimsPrincipal)HttpContext.User).FindFirst("AcessToken").Value;

            await _photoAlbumService.DeletePhotoByIdAsync(id, token);

            return RedirectToAction("UserPageManage", new { username = User.Identity.Name });
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> EditPhoto(int id, string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            var token = ((ClaimsPrincipal)HttpContext.User).FindFirst("AcessToken").Value;

            EditPhotoViewModel editPhotoViewModel = null;
            EditPhotoDto editPhotoDto = await _photoAlbumService.GetEditPhotoByIdAsync(id, token);
            if(editPhotoDto != null)
            {
                editPhotoViewModel = Mapper.Map<EditPhotoViewModel>(editPhotoDto);
            }

            return PartialView(editPhotoViewModel);
        }
        
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditPhoto(EditPhotoViewModel editPhotoViewModel, string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            if (!ModelState.IsValid)
            {
                return PartialView(editPhotoViewModel);
            }

            var token = ((ClaimsPrincipal)HttpContext.User).FindFirst("AcessToken").Value;

            EditPhotoDto editPhotoDto = new EditPhotoDto
            {
                Id = editPhotoViewModel.Id,
                Title = editPhotoViewModel.Title,
                Description = editPhotoViewModel.Description
            };

            await _photoAlbumService.EditPhotoAsync(editPhotoDto, token);
            
            // Return EmptyResult() for modal window
            return new EmptyResult();
        }

        public async Task<ActionResult> GetPhotoRating(int id)
        {
            var rating = await _photoAlbumService.GetPhotoRatingAsync(id);

            return Json(rating, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetPhotos(int lastRowId, bool isHistoryBack, SortOrder sorting)
        {
            int pageSize = int.Parse( ConfigurationManager.AppSettings["PageSize"] );

            var photos = await _photoAlbumService.GetPhotosAsync(new PagingParametersDto
            {
                PageNumber = lastRowId,
                PageSize = pageSize,
                Sorting = sorting
            });

            return Json(photos, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetUserPhotos(int lastRowId, bool isHistoryBack, SortOrder sorting, string userName)
        {
            int pageSize = int.Parse( ConfigurationManager.AppSettings["PageSize"] );

            var photos = await _photoAlbumService.GetUserPhotosAsync(new PagingParametersDto
            {
                PageNumber = lastRowId,
                PageSize = pageSize,
                Sorting = sorting
            },
            userName);

            return Json(photos, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetAllUserVotes()
        {
            var token = ((ClaimsPrincipal)HttpContext.User).FindFirst("AcessToken").Value;

            var userVotes = await _photoAlbumService.GetUserVotesAsync(token);

            return Json(userVotes, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetUserVotes(int id)
        {
            var token = ((ClaimsPrincipal)HttpContext.User).FindFirst("AcessToken").Value;

            var userVotes = await _photoAlbumService.GetUserVotesAsync(token, id);

            return Json(userVotes, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> CastPhotoVote(PhotoVoteViewModel model)
        {
            var token = ((ClaimsPrincipal)HttpContext.User).FindFirst("AcessToken").Value;

            var photoVoteDto = Mapper.Map<PhotoVoteDto>(model);

            var result = await _photoAlbumService.CastPhotoVoteAsync(photoVoteDto, token);

            return new HttpStatusCodeResult(result);
        }
    }
}