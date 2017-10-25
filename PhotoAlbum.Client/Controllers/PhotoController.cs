using PhotoAlbum.Client.Dto;
using PhotoAlbum.Client.Model.Services;
using PhotoAlbum.Client.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhotoAlbum.Client.Controllers
{
    public class PhotoController : Controller
    {
        private PhotoAlbumService _photoAlbumService = new PhotoAlbumService();

        // GET: Photo
        public ActionResult Index()
        {
            var photos = _photoAlbumService.GetAllPhotos();
            return View(photos);
        }

        public ActionResult AddPhoto()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddPhoto(AddPhotoModel addPhotoModel)
        {
            if (ModelState.IsValid && addPhotoModel.Image != null)
            {
                byte[] imageData = null;
                // считываем переданный файл в массив байтов
                using (var binaryReader = new BinaryReader(addPhotoModel.Image.InputStream))
                {
                    imageData = binaryReader.ReadBytes(addPhotoModel.Image.ContentLength);
                }
                // установка массива байтов
                var addPhotoDto = new AddPhotoDto();
                addPhotoDto.Image = imageData;
                addPhotoDto.Title = addPhotoModel.Title;
                addPhotoDto.Description = addPhotoModel.Description;

                _photoAlbumService.AddPhoto(addPhotoDto);

                return RedirectToAction("Index");
            }
            return View();
        }

        public void Delete(int photoId)
        {
            _photoAlbumService.DeletePhoto(photoId);
        }
    }
}