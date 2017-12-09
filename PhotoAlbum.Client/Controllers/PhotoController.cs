﻿using AutoMapper;
using PhotoAlbum.Client.BusinessServices.Interfaces;
using PhotoAlbum.Client.BusinessServices.Services;
using PhotoAlbum.Client.Dto;
//using PhotoAlbum.Client.Model.Services;
using PhotoAlbum.Client.Models;
using PhotoAlbum.Common.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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



        public async Task Test()
        {
            await GetPhotos(1, false);
            //await _photoAlbumService.Test();
        }



        // GET: Photo
        public async Task<ActionResult> Index()
        {
            //List<PhotoViewModel> photos = new List<PhotoViewModel>();
            ////List<PhotoDto> photosDto = await _photoAlbumService.GetAllPhotosAsync();
            //List<PhotoDto> photosDto = await _photoAlbumService.GetPhotosAsync(new PagingParametersDto
            //{
            //    PageNumber = 1,
            //    PageSize = 3
            //});

            //// Mapping
            //if (photosDto != null)
            //{
            //    foreach(var photoDto in photosDto)
            //    {
            //        photos.Add(new PhotoViewModel
            //        {
            //            Id = photoDto.Id,
            //            Title = photoDto.Title,
            //            Description = photoDto.Description,
            //            CreationDate = photoDto.CreationDate,
            //            OwnerName = photoDto.OwnerName,
            //            Rating = photoDto.Rating
            //        });
            //    }
            //}

            return View(/*photos*/);
        }

        public ActionResult CreatePhoto()
        {
            return View();
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

                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<ActionResult> GetImageById(int id)
        {
            var image = await _photoAlbumService.GetImageByIdAsync(id);
            return File(image.Image, image.ImageMimeType);
        }

        public async Task<ActionResult> DeletePhotoById(int id)
        {
            var token = ((ClaimsPrincipal)HttpContext.User).FindFirst("AcessToken").Value;
            await _photoAlbumService.DeletePhotoByIdAsync(id, token);
            return RedirectToAction("Index");
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
            return View(editPhotoViewModel);
        }

        //[HttpPut]
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
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> GetPhotoRating(int id)
        {
            var rating = await _photoAlbumService.GetPhotoRatingAsync(id);
            return Json(rating.Rating);
        }

        public async Task<ActionResult> GetPhotos(int lastRowId, bool isHistoryBack)
        {
            var photos = await _photoAlbumService.GetPhotosAsync(new PagingParametersDto
            {
                PageNumber = lastRowId,
                PageSize = 5
            });
            return Json(photos, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetUserVotes()
        {
            var token = ((ClaimsPrincipal)HttpContext.User).FindFirst("AcessToken").Value;

            var userVotes = await _photoAlbumService.GetUserVotesAsync(token);
            return Json(userVotes, JsonRequestBehavior.AllowGet);
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