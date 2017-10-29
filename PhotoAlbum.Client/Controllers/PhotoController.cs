﻿using PhotoAlbum.Client.BusinessServices.Interfaces;
using PhotoAlbum.Client.BusinessServices.Services;
using PhotoAlbum.Client.Dto;
//using PhotoAlbum.Client.Model.Services;
using PhotoAlbum.Client.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PhotoAlbum.Client.Controllers
{
    public class PhotoController : Controller
    {
        //private PhotoAlbumService _photoAlbumService = new PhotoAlbumService();
        private IPhotoAlbumService _photoAlbumService = new PhotoAlbumService();

        public async Task<HttpResponseMessage> Test()
        {
            var response = await _photoAlbumService.Test();
            var result = response.Content;
            
            return response;
        }

        // GET: Photo
        public ActionResult Index()
        {
            //var photos = _photoAlbumService.GetAllPhotos();
            List<PhotoModel> photos = null;
            return View(photos);
        }

        public ActionResult CreatePhoto()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreatePhoto(CreatePhotoModel createPhotoModel)
        {
            if (ModelState.IsValid && createPhotoModel.Image != null)
            {
                byte[] imageData = null;
                // считываем переданный файл в массив байтов
                using (var binaryReader = new BinaryReader(createPhotoModel.Image.InputStream))
                {
                    imageData = binaryReader.ReadBytes(createPhotoModel.Image.ContentLength);
                }
                // установка массива байтов
                var createPhotoDto = new CreatePhotoDto();
                createPhotoDto.Image = imageData;
                createPhotoDto.Title = createPhotoModel.Title;
                createPhotoDto.Description = createPhotoModel.Description;

                await _photoAlbumService.CreatePhoto(createPhotoDto);

                return RedirectToAction("Index");
            }
            return View();
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