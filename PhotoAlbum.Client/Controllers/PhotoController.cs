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
        // GET: Photo
        public ActionResult Index()
        {
            return View();
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
                photo.Image = imageData;

                _photoAlbumService.AddPhoto(photo.Image);

                return RedirectToAction("Index");
            }
            return View(photo);
        }

        public void Delete(int id)
        {
            _photoAlbumService.DeletePhoto(id);
        }
    }
}