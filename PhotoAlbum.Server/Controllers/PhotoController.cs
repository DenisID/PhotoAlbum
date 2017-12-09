﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PhotoAlbum.Common.Enums;
using PhotoAlbum.Common.ErrorCodes;
using PhotoAlbum.Common.Exceptions;
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
    public class PhotoController : BaseController
    {
        private readonly IPhotoAlbumService _photoAlbumService;

        public PhotoController(IPhotoAlbumService photoAlbumService)
        {
            _photoAlbumService = photoAlbumService;
        }



        [HttpGet]
        [Route("api/test")]
        public HttpResponseMessage Test()
        {
            //throw new Exception("TestEx");
            throw new PhotoNotFoundException(ErrorCodes.PhotoNotFound);
            return Success(_photoAlbumService.GetPhotosCount());
        }



        [HttpGet]
        [Route("api/allphotos")]
        public HttpResponseMessage GetAllPhotos()
        {
            return Success(_photoAlbumService.GetAllPhotos());
        }

        [HttpGet]
        [Route("api/photo")]
        public HttpResponseMessage GetPhotos([FromUri]PagingParametersDto pagingParameters)
        {
            //throw new PhotoNotFoundException(ErrorCodes.PhotoNotFound);
            return Success(_photoAlbumService.GetPhotos(pagingParameters));
        }

        [HttpGet]
        [Route("api/photo/count")]
        public HttpResponseMessage GetPhotosCount()
        {
            return Success(_photoAlbumService.GetPhotosCount());
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
            var user = RequestContext.Principal.Identity.Name;
            var userId = User.Identity.GetUserId();
            createPhotoDto.UserId = userId;

            return Success(_photoAlbumService.CreatePhoto(createPhotoDto));
        }

        [HttpDelete]
        [Route("api/photo/{id}")]
        [Authorize]
        public HttpResponseMessage DeletePhotoById([FromUri] int id)
        {
            //var user = User.Identity.Name;
            if (!_photoAlbumService.IsPhotoOwner(User.Identity.GetUserId(), id))
            {
                throw new NotEnoughRightsException(ErrorCodes.NotEnoughRights);
            }

            _photoAlbumService.DeletePhotoById(id);

            return Success();
        }

        [HttpPut]
        [HttpPost]
        [Route("api/photo/editphoto")]
        [Authorize]
        public HttpResponseMessage EditPhoto([FromBody] EditPhotoDto editPhotoDto)
        {
            //var user = User.Identity.Name;
            if (!_photoAlbumService.IsPhotoOwner(User.Identity.GetUserId(), editPhotoDto.Id))
            {
                throw new NotEnoughRightsException(ErrorCodes.NotEnoughRights);
            }

            _photoAlbumService.EditPhoto(editPhotoDto);
            return Success();
        }

        [HttpGet]
        [Route("api/photo/editphoto/{id}")]
        public HttpResponseMessage GetEditPhotoById(int id)
        {
            return Success(_photoAlbumService.GetEditPhotoById(id));
        }
        
        [HttpPost]
        [Route("api/photo/vote")]
        [Authorize]
        public HttpResponseMessage CastVote([FromBody]CastPhotoVoteDto photoVoteDto)
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

        [HttpGet]
        [Route("api/photo/vote")]
        [Authorize]
        public HttpResponseMessage GetUserVotes()
        {
            var userId = User.Identity.GetUserId();

            return Success(_photoAlbumService.GetUserVotes(userId));
        }
    }
}
