﻿using PhotoAlbum.Client.Dto;
using PhotoAlbum.Client.Model.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhotoAlbum.Client.Controllers
{
    public class TestController : Controller
    {
        private PhotoAlbumService _photoAlbumService = new PhotoAlbumService();

        // GET: Test
        public ActionResult Index()
        {
            var res = _photoAlbumService.GetAllPhoto();
            return View(res);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(PhotoContentDto pic, HttpPostedFileBase uploadImage)
        {
            if (ModelState.IsValid && uploadImage != null)
            {
                

                byte[] imageData = null;
                // считываем переданный файл в массив байтов
                using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                {
                    imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
                }
                // установка массива байтов
                pic.Image = imageData;

                _photoAlbumService.AddPhoto(pic.Image);

                return RedirectToAction("Index");
            }
            return View(pic);
        }

        public void Delete(int id)
        {
            _photoAlbumService.DeletePhoto(id);
        }
    }
}