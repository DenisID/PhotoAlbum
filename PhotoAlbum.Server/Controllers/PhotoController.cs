using PhotoAlbum.Server.Dto;
using PhotoAlbum.Server.Model.Interfaces;
using PhotoAlbum.Server.Model.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace PhotoAlbum.Server.Controllers
{
    public class PhotoController : ApiController
    {
        private IPhotoAlbumService _photoAlbumService = new PhotoAlbumService();

        public class ResponseDTO
        {
            public string value;
        }

        public ResponseDTO GetTest()
        {
            var result = new ResponseDTO();
            result.value = "Test OK";
            return result;
        }

        [HttpPost]
        public IHttpActionResult CreatePhoto([FromBody] CreatePhotoDto createPhotoDto)
        {
            return Ok(_photoAlbumService.CreatePhoto(createPhotoDto));
        }

        //// GET api/values
        //public IEnumerable<string> GetAllPhoto()
        //{         
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/values/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/photo
        //public void PostPhoto([FromBody]AddPhotoDto addPhotoDto)
        //{
        //    if (ModelState.IsValid && addPhotoDto.Image != null)
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

        //// PUT api/values/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/values/5
        //public void Delete(int id)
        //{
        //}
    }
}
