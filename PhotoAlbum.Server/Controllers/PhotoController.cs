using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PhotoAlbum.Server.Dto;
using PhotoAlbum.Server.Model.Interfaces;
using PhotoAlbum.Server.Model.Managers;
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
    //[Authorize]
    public class PhotoController : BaseController
    {
        private readonly IPhotoAlbumService _photoAlbumService;

        public PhotoController(IPhotoAlbumService photoAlbumService)
        {
            _photoAlbumService = photoAlbumService;
        }

        //public class ResponseDTO
        //{
        //    public string value;
        //}

        //public ResponseDTO GetTest()
        //{
        //    var result = new ResponseDTO();
        //    result.value = "Test OK";
        //    return result;
        //}

        [HttpGet]
        [Route("api/photo")]
        public HttpResponseMessage GetAllPhotos()
        {
            //return Request.CreateResponse(HttpStatusCode.OK, _photoAlbumService.GetAllPhotos());
            return Success(_photoAlbumService.GetAllPhotos());
        }

        [HttpGet]
        [Route("api/photo/image/{id}")]
        public HttpResponseMessage GetImageById([FromUri] int id)
        {
            return Success(_photoAlbumService.GetImageById(id));
        }

        [HttpPost]
        [Route("api/photo")]
        [Authorize]
        public HttpResponseMessage CreatePhoto([FromBody] CreatePhotoDto createPhotoDto)
        {
            var req = Request;
            var reqC = RequestContext;
            var reqH = Request.Headers;
            var reqHA = Request.Headers.Authorization;

            var user = RequestContext.Principal.Identity.Name;
            var userId = User.Identity.GetUserId();
            createPhotoDto.UserId = userId;

            //return Ok(_photoAlbumService.CreatePhoto(createPhotoDto));
            return Success(_photoAlbumService.CreatePhoto(createPhotoDto));
        }

        [HttpDelete]
        [Route("api/photo/{id}")]
        [Authorize]
        public HttpResponseMessage DeletePhotoById([FromUri] int id)
        {
            try
            {
                var user = User.Identity.Name;
                if (!_photoAlbumService.IsPhotoOwner(User.Identity.GetUserId(), id))
                {
                    throw new Exception("Не достаточно прав");
                }

                //if (User.Identity.GetUserId() == _photoAlbumService.GetEditPhotoById(id))

                _photoAlbumService.DeletePhotoById(id);
                return Success();
            }
            catch(Exception ex)
            {
                return Error(ex);
            }
        }

        [HttpPut]
        [HttpPost]
        [Route("api/photo/editphoto")]
        [Authorize]
        public HttpResponseMessage EditPhoto([FromBody] EditPhotoDto editPhotoDto)
        {
            try
            {
                var user = User.Identity.Name;
                if (!_photoAlbumService.IsPhotoOwner(User.Identity.GetUserId(), editPhotoDto.Id))
                {
                    throw new Exception("Не достаточно прав");
                }

                _photoAlbumService.EditPhoto(editPhotoDto);
                return Success();
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

        [HttpGet]
        [Route("api/photo/editphoto/{id}")]
        [Authorize]
        public HttpResponseMessage GetEditPhotoById(int id)
        {
            try
            {
                return Success(_photoAlbumService.GetEditPhotoById(id));
            }
            catch(Exception ex)
            {
                return Error(ex);
            }
        }
        
        [HttpPost]
        [Route("api/photo/vote")]
        [Authorize]
        public HttpResponseMessage CastVote([FromBody]CastPhotoVoteDto photoVoteDto)
        {
            try
            {
                var castPhotoVoteDto = new PhotoVoteDto
                {
                    PhotoId = photoVoteDto.PhotoId,
                    UserId = User.Identity.GetUserId(),
                    Rating = photoVoteDto.Rating
                };
                _photoAlbumService.CastPhotoVote(castPhotoVoteDto);
                return Success();
            }
            catch(Exception ex)
            {
                return Error(ex);
            }
        }

        [HttpGet]
        [Route("api/photo/rating/{id}")]
        public HttpResponseMessage GetPhotoRating(int id)
        {
            try
            {
                return Success(_photoAlbumService.GetPhotoRating(id));
            }
            catch(Exception ex)
            {
                return Error(ex);
            }
        }

        [HttpGet]
        [Route("api/photo/vote")]
        [Authorize]
        public HttpResponseMessage GetUserVotes()
        {
            try
            {
                var userId = User.Identity.GetUserId();

                return Success(_photoAlbumService.GetUserVotes(userId));
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
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
