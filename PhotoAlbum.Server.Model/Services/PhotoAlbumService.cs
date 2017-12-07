using AutoMapper;
using DelegateDecompiler;
using PhotoAlbum.Common.Enums;
using PhotoAlbum.Common.ErrorCodes;
using PhotoAlbum.Common.Exceptions;
using PhotoAlbum.Server.Dto;
using PhotoAlbum.Server.Model.Data;
using PhotoAlbum.Server.Model.Entities;
using PhotoAlbum.Server.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAlbum.Server.Model.Services
{
    public class PhotoAlbumService : IPhotoAlbumService
    {
        private PhotoAlbumContext _photoAlbumContext = new PhotoAlbumContext();

        public int CreatePhoto(CreatePhotoDto createPhotoDto)
        {
            var photo = new Photo();
            photo.CreationDate = DateTime.Now;
            photo.Title = createPhotoDto.Title;
            photo.Description = createPhotoDto.Description;

            var user = _photoAlbumContext.Users.Find(createPhotoDto.UserId);
            photo.User = user;

            _photoAlbumContext.Photos.Add(photo);
            _photoAlbumContext.SaveChanges();

            var photoContent = new PhotoContent();
            photoContent.Id = photo.Id;
            photoContent.Image = createPhotoDto.Image;
            photoContent.ImageMimeType = createPhotoDto.ImageMimeType;

            _photoAlbumContext.PhotoContents.Add(photoContent);
            _photoAlbumContext.SaveChanges();

            return photo.Id;
        }

        public List<PhotoDto> GetAllPhotos()
        {
            var photosFromDb = _photoAlbumContext.Photos;

            if(photosFromDb == null)
            {
                throw new PhotoNotFoundException(ErrorCodes.NoPhotosInDatabase);
            }

            var photos = new List<PhotoDto>();
            foreach (var photoFromDb in photosFromDb)
            {
                photos.Add(Mapper.Map<Photo, PhotoDto>(photoFromDb));
            }

            return photos;
        }

        public List<PhotoDto> GetPhotos(PagingParametersDto pagingParameters)
        {
            IQueryable<Photo> photosFromDb = null;

            switch (pagingParameters.Sorting)
            {
                case SortOrder.ByCreationDate:
                    photosFromDb = _photoAlbumContext.Photos.OrderByDescending(x => x.CreationDate);
                    break;

                case SortOrder.ByRating:
                    photosFromDb = (_photoAlbumContext.Photos.OrderByDescending(x => x.Rating)).Decompile();
                    break;

                default:
                    throw new Exception("PhotoAlbumService.GetPhotos method - sorting param error");
            }

            photosFromDb = photosFromDb.Skip((pagingParameters.PageNumber - 1) * pagingParameters.PageSize)
                                       .Take(pagingParameters.PageSize);

            if (photosFromDb == null)
            {
                throw new PhotoNotFoundException(ErrorCodes.NoPhotosInDatabase);
            }

            var photos = new List<PhotoDto>();
            foreach (var photoFromDb in photosFromDb)
            {
                photos.Add(Mapper.Map<Photo, PhotoDto>(photoFromDb));
            }

            return photos;
        }

        public int GetPhotosCount()
        {
            return _photoAlbumContext.Photos.Count();

        }

        public PhotoDto GetPhotoById(int photoId)
        {
            throw new NotImplementedException();
        }

        public ImageDto GetImageById(int imageId)
        {
            var imageFromDb = _photoAlbumContext.PhotoContents.Find(imageId);

            if(imageFromDb == null)
            {
                throw new PhotoNotFoundException(ErrorCodes.PhotoNotFound);
            }

            var image = Mapper.Map<PhotoContent, ImageDto>(imageFromDb);

            return image;
        }

        public void DeletePhotoById(int photoId)
        {
            var photo = _photoAlbumContext.Photos.Find(photoId);
            var photoContent = _photoAlbumContext.PhotoContents.Find(photoId);

            if (photo == null || photoContent == null)
            {
                throw new PhotoNotFoundException(ErrorCodes.PhotoNotFound);
            }

            _photoAlbumContext.Entry(photoContent).State = EntityState.Deleted;
            _photoAlbumContext.Entry(photo).State = EntityState.Deleted;
            _photoAlbumContext.SaveChanges();
        }

        public void EditPhoto(EditPhotoDto editPhotoDto)
        {
            var photo = _photoAlbumContext.Photos.Find(editPhotoDto.Id);

            if (photo == null)
            {
                throw new PhotoNotFoundException(ErrorCodes.PhotoNotFound);
            }

            photo.Title = editPhotoDto.Title;
            photo.Description = editPhotoDto.Description;

            _photoAlbumContext.Entry(photo).State = EntityState.Modified;
            _photoAlbumContext.SaveChanges();
        }

        public EditPhotoDto GetEditPhotoById(int editPhotoId)
        {
            var photo = _photoAlbumContext.Photos.Find(editPhotoId);

            if (photo == null)
            {
                throw new PhotoNotFoundException(ErrorCodes.PhotoNotFound);
            }

            EditPhotoDto editPhotoDto = null;
            editPhotoDto = Mapper.Map<Photo, EditPhotoDto>(photo);

            return editPhotoDto;
        }

        public void CastPhotoVote(PhotoVoteDto castPhotoVoteDto)
        {
            var photo = _photoAlbumContext.Photos.Find(castPhotoVoteDto.PhotoId);
            if (photo == null)
            {
                throw new PhotoNotFoundException(ErrorCodes.PhotoNotFound);
            }

            var user = _photoAlbumContext.Users.Find(castPhotoVoteDto.UserId);
            if (user == null)
            {
                throw new UserNotFoundException(ErrorCodes.UserNotFound);
            }

            var photoVote = _photoAlbumContext.PhotoVotes.Where(x => x.UserId == castPhotoVoteDto.UserId)
                                                         .Where(x => x.PhotoId == castPhotoVoteDto.PhotoId)
                                                         .FirstOrDefault();

            if (photoVote == null)
            {
                _photoAlbumContext.PhotoVotes.Add(Mapper.Map<PhotoVoteDto, PhotoVote>(castPhotoVoteDto));
            }
            else
            {
                photoVote.Rating = castPhotoVoteDto.Rating;
                _photoAlbumContext.Entry(photoVote).State = EntityState.Modified;
            }
            _photoAlbumContext.SaveChanges();
        }

        //public PhotoRatingDto GetPhotoRating(int photoId)
        //{
        //    var ratingSum = _photoAlbumContext.PhotoVotes.Where(x => x.PhotoId == photoId)
        //                                                 .Average(x => x.Rating);
        //    // Mapping
        //    return new PhotoRatingDto
        //    {
        //        Rating = ratingSum
        //    };
        //    //throw new NotImplementedException();
        //}

        public List<PhotoVoteDto> GetUserVotes(string userId)
        {
            var photoVotes = _photoAlbumContext.PhotoVotes.Where(x => x.UserId == userId).ToList();

            List<PhotoVoteDto> userVotes = new List<PhotoVoteDto>();
            foreach(var element in photoVotes)
            {
                userVotes.Add(Mapper.Map<PhotoVote, PhotoVoteDto>(element));
            }

            return userVotes;
        }

        public bool IsPhotoOwner(string userId, int photoId)
        {
            var user = _photoAlbumContext.Users.Find(userId);
            if (user == null)
            {
                throw new UserNotFoundException(ErrorCodes.UserNotFound);
            }

            var photo = _photoAlbumContext.Photos.Find(photoId);
            if (user == null)
            {
                throw new PhotoNotFoundException(ErrorCodes.PhotoNotFound);
            }

            return (photo.User == user) ? true : false;
        }

        //public Photo GetPhotoById(int photoId)
        //{
        //    if (photoId < 0)
        //    {
        //        throw new ArgumentNullException(nameof(photoId));
        //    }

        //    var photo = _photoAlbumContext.Photos.Find(photoId);
        //    //if (photo == null)
        //    //{
        //    //    throw PhotoNotFoundException.FromPhotoId(photoId);
        //    //}
        //    //photo.ValidateEntity();
        //    return photo;
        //}
    }
}
