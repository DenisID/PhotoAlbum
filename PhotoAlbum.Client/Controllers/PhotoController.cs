﻿using AutoMapper;
using Newtonsoft.Json;
using PhotoAlbum.Client.BusinessServices.Interfaces;
using PhotoAlbum.Client.BusinessServices.Services;
using PhotoAlbum.Client.Dto;
//using PhotoAlbum.Client.Model.Services;
using PhotoAlbum.Client.Models;
using PhotoAlbum.Common.Enums;
using PhotoAlbum.Common.ErrorCodes;
using PhotoAlbum.Common.Exceptions;
using PhotoAlbum.Common.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PhotoAlbum.Client.Controllers
{
    public class PhotoController : Controller
    {
        private readonly IPhotoAlbumService _photoAlbumService;

        public PhotoController(IPhotoAlbumService photoAlbumService)
        {
            _photoAlbumService = photoAlbumService;
        }

        

        public async Task Test(string username)
        {
            
        }



        // GET: Photo
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
            if(username != User.Identity.Name)
            {
                throw new UserIsNotAuthorizedException(ErrorCodes.UserIsNotAuthorized);
            }

            var model = new UserPageViewModel() { UserName = User.Identity.Name };

            return View(model);
        }

        public ActionResult CreatePhoto()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<ActionResult> CreatePhoto(CreatePhotoViewModel createPhotoViewModel)
        {
            var token = ((ClaimsPrincipal)HttpContext.User).FindFirst("AcessToken").Value;

            if (ModelState.IsValid && createPhotoViewModel.Image != null)
            {
                byte[] imageData = null;
                // считываем переданный файл в массив байтов
                using (var binaryReader = new BinaryReader(createPhotoViewModel.Image.InputStream))
                {
                    imageData = binaryReader.ReadBytes(createPhotoViewModel.Image.ContentLength);
                }
                // установка массива байтов
                var createPhotoDto = new CreatePhotoDto();
                createPhotoDto.Image = imageData;
                createPhotoDto.ImageMimeType = createPhotoViewModel.Image.ContentType;
                createPhotoDto.Title = createPhotoViewModel.Title;
                createPhotoDto.Description = createPhotoViewModel.Description;

                await _photoAlbumService.CreatePhotoAsync(createPhotoDto, token);
                
                return RedirectToAction("UserPageManage", new { username = User.Identity.Name });
            }
            return View();
        }

        //[OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public async Task<ActionResult> GetImageById(int id)
        {
            var image = await _photoAlbumService.GetImageByIdAsync(id);

            var requestedETag = Request.Headers["If-None-Match"];

            string unifiedData = JsonConvert.SerializeObject(image);
            var responseETag = ETagHashCreator.ComputeHash(unifiedData);
            if (requestedETag == responseETag)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotModified);
            }
                
            Response.Cache.SetCacheability(HttpCacheability.ServerAndPrivate);
            Response.Cache.SetETag(responseETag);

            return File(image.Image, image.ImageMimeType);
        }

        public async Task<ActionResult> DeletePhotoById(int id)
        {
            var token = ((ClaimsPrincipal)HttpContext.User).FindFirst("AcessToken").Value;
            await _photoAlbumService.DeletePhotoByIdAsync(id, token);
            return RedirectToAction("UserPageManage", new { username = User.Identity.Name });
        }

        [HttpGet]
        public async Task<ActionResult> EditPhoto(int id)
        {
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
        public async Task<ActionResult> EditPhoto(EditPhotoViewModel editPhotoViewModel)
        {
            var token = ((ClaimsPrincipal)HttpContext.User).FindFirst("AcessToken").Value;

            EditPhotoDto editPhotoDto = new EditPhotoDto
            {
                Id = editPhotoViewModel.Id,
                Title = editPhotoViewModel.Title,
                Description = editPhotoViewModel.Description
            };

            await _photoAlbumService.EditPhotoAsync(editPhotoDto, token);
            return RedirectToAction("UserPageManage", new { username = User.Identity.Name });
        }

        public async Task<ActionResult> GetPhotoRating(int id)
        {
            var rating = await _photoAlbumService.GetPhotoRatingAsync(id);
            return Json(rating, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetPhotos(int lastRowId, bool isHistoryBack, SortOrder sorting)
        {
            var photos = await _photoAlbumService.GetPhotosAsync(new PagingParametersDto
            {
                PageNumber = lastRowId,
                PageSize = 5,
                Sorting = sorting
            });
            return Json(photos, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetUserPhotos(int lastRowId, bool isHistoryBack, SortOrder sorting, string userName)
        {
            var photos = await _photoAlbumService.GetUserPhotosAsync(new PagingParametersDto
            {
                PageNumber = lastRowId,
                PageSize = 5,
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
        public async Task<ActionResult> CastPhotoVote(PhotoVoteViewModel model)
        {
            var token = ((ClaimsPrincipal)HttpContext.User).FindFirst("AcessToken").Value;

            var photoVoteDto = Mapper.Map<PhotoVoteDto>(model);

            var result = await _photoAlbumService.CastPhotoVoteAsync(photoVoteDto, token);
            return null;
        }

        //[HttpPost]
        //public ActionResult AddPhoto(AddPhotoModel addPhotoModel)
        //{
        //    if (ModelState.IsValid && addPhotoModel.Image != null)
        //    {
        //        byte[] imageData = null;
        //        // считываем переданный файл в массив байтов
        //        using (var binaryReader = new BinaryReader(addPhotoModel.Image.InputStream))
        //        {
        //            imageData = binaryReader.ReadBytes(addPhotoModel.Image.ContentLength);
        //        }
        //        // установка массива байтов
        //        var addPhotoDto = new AddPhotoDto();
        //        addPhotoDto.Image = imageData;
        //        addPhotoDto.Title = addPhotoModel.Title;
        //        addPhotoDto.Description = addPhotoModel.Description;

        //        _photoAlbumService.AddPhoto(addPhotoDto);

        //        return RedirectToAction("Index");
        //    }
        //    return View();
        //}

        //public void Delete(int photoId)
        //{
        //    _photoAlbumService.DeletePhoto(photoId);
        //}
    }
}