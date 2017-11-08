using AutoMapper;
using PhotoAlbum.Client.BusinessServices.Interfaces;
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
        //private IPhotoAlbumService _photoAlbumService = new PhotoAlbumService();
        private readonly IPhotoAlbumService _photoAlbumService;

        public PhotoController(IPhotoAlbumService photoAlbumService)
        {
            _photoAlbumService = photoAlbumService;
        }

        //public async Task<HttpResponseMessage> Test()
        //{
        //    var response = await _photoAlbumService.Test();
        //    var result = response.Content;

        //    return response;
        //}

        // GET: Photo
        public async Task<ActionResult> Index()
        {
            //var photos = _photoAlbumService.GetAllPhotos();
            List<PhotoModel> photos = new List<PhotoModel>();
            List<PhotoDto> photosDto = await _photoAlbumService.GetAllPhotos();

            // Mapping
            if(photosDto != null)
            {
                foreach(var photoDto in photosDto)
                {
                    photos.Add(new PhotoModel
                    {
                        Id = photoDto.Id,
                        Title = photoDto.Title,
                        Description = photoDto.Description,
                        CreationDate = photoDto.CreationDate,
                        Image = photoDto.Image,
                        ImageMimeType = photoDto.ImageMimeType
                    });
                }
            }

            return View(photos);
        }

        public ActionResult CreatePhoto()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreatePhoto(CreatePhotoViewModel createPhotoViewModel)
        {
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

                await _photoAlbumService.CreatePhoto(createPhotoDto);

                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<ActionResult> GetImageById(int id)
        {
            var image = await _photoAlbumService.GetImageById(id);
            return File(image.Image, image.ImageMimeType);
        }

        public async Task<ActionResult> DeletePhotoById(int id)
        {
            await _photoAlbumService.DeletePhotoById(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> EditPhoto(int id)
        {
            EditPhotoViewModel editPhotoViewModel = null;
            EditPhotoDto editPhotoDto = await _photoAlbumService.GetEditPhotoById(id);
            if(editPhotoDto != null)
            {
                // Mapping
                //editPhotoViewModel = new EditPhotoViewModel
                //{
                //    Id = editPhotoDto.Id,
                //    Title = editPhotoDto.Title,
                //    Description = editPhotoDto.Description
                //};

                editPhotoViewModel = Mapper.Map<EditPhotoViewModel>(editPhotoDto);
            }
            return View(editPhotoViewModel);
        }

        //[HttpPut]
        [HttpPost]
        public async Task<ActionResult> EditPhoto(EditPhotoViewModel editPhotoViewModel)
        {
            EditPhotoDto editPhotoDto = new EditPhotoDto
            {
                Id = editPhotoViewModel.Id,
                Title = editPhotoViewModel.Title,
                Description = editPhotoViewModel.Description
            };

            await _photoAlbumService.EditPhoto(editPhotoDto);
            return RedirectToAction("Index");
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