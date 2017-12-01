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
            var photosFromDb = _photoAlbumContext.Photos/*.ToList()*/;
            var photos = new List<PhotoDto>();
            if(photosFromDb != null)
            {
                foreach(var photoFromDb in photosFromDb)
                {
                    // Mapping
                    photos.Add(new PhotoDto
                    {
                        Id = photoFromDb.Id,
                        Title = photoFromDb.Title,
                        Description = photoFromDb.Description,
                        CreationDate = photoFromDb.CreationDate,
                        OwnerName = photoFromDb.User.UserName,
                        //Image = photoFromDb.PhotoContent.Image,
                        //ImageMimeType = photoFromDb.PhotoContent.ImageMimeType
                    });
                    // TODO : bad code
                    photos.Last().Rating = GetPhotoRating(photos.Last().Id).Rating;
                }
            }
            
            return photos;
        }

        public PhotoDto GetPhotoById(int photoId)
        {
            throw new NotImplementedException();
        }

        public ImageDto GetImageById(int imageId)
        {
            var imageFromDb = _photoAlbumContext.PhotoContents.Find(imageId);
            var image = new ImageDto
            {
                Image = imageFromDb.Image,
                ImageMimeType = imageFromDb.ImageMimeType
            };

            return image;
        }

        public void DeletePhotoById(int photoId)
        {
            var photo = _photoAlbumContext.Photos.Find(photoId);
            var photoContent = _photoAlbumContext.PhotoContents.Find(photoId);
            _photoAlbumContext.Entry(photoContent).State = EntityState.Deleted;
            _photoAlbumContext.Entry(photo).State = EntityState.Deleted;
            _photoAlbumContext.SaveChanges();
        }

        public void EditPhoto(EditPhotoDto editPhotoDto)
        {
            //var photo = GetPhotoById(photoDto.Id);
            var photo = _photoAlbumContext.Photos.Find(editPhotoDto.Id);
            if(photo != null)
            {
                // Mapping
                photo.Title = editPhotoDto.Title;
                photo.Description = editPhotoDto.Description;

                _photoAlbumContext.Entry(photo).State = EntityState.Modified;
                _photoAlbumContext.SaveChanges();
            }
            
        }

        public EditPhotoDto GetEditPhotoById(int editPhotoId)
        {
            var photo = _photoAlbumContext.Photos.Find(editPhotoId);
            EditPhotoDto editPhotoDto = null;
            
            // TODO : add ex

            //if(photo == null)
            //{
            //    throw new Exception("Photo Not Found");
            //}
            if (photo != null)
            {
                // Mapping
                editPhotoDto = new EditPhotoDto {
                    Id = photo.Id,
                    Title = photo.Title,
                    Description = photo.Description
                };
            }

            return editPhotoDto;
        }

        public void CastPhotoVote(PhotoVoteDto castPhotoVoteDto)
        {
            var photoVote = _photoAlbumContext.PhotoVotes.Where(x => x.UserId == castPhotoVoteDto.UserId)
                                                         .Where(x => x.PhotoId == castPhotoVoteDto.PhotoId)
                                                         .FirstOrDefault();

            if (photoVote == null)
            {
                _photoAlbumContext.PhotoVotes.Add(new PhotoVote
                {
                    PhotoId = castPhotoVoteDto.PhotoId,
                    UserId = castPhotoVoteDto.UserId,
                    Rating = castPhotoVoteDto.Rating
                });
            }
            else
            {
                photoVote.Rating = castPhotoVoteDto.Rating;
                _photoAlbumContext.Entry(photoVote).State = EntityState.Modified;
            }
            _photoAlbumContext.SaveChanges();
        }

        public PhotoRatingDto GetPhotoRating(int photoId)
        {
            var ratingSum = _photoAlbumContext.PhotoVotes.Where(x => x.PhotoId == photoId)
                                                         .Average(x => x.Rating);
            // Mapping
            return new PhotoRatingDto
            {
                Rating = ratingSum
            };
            //throw new NotImplementedException();
        }

        public List<PhotoVoteDto> GetUserVotes(string userId)
        {
            var photoVotes = _photoAlbumContext.PhotoVotes.Where(x => x.UserId == userId).ToList();

            // Mapping
            if(photoVotes.Count == 0)
            {
                throw new Exception("Votes not found");
            }

            List<PhotoVoteDto> returnedValue = new List<PhotoVoteDto>();
            foreach(var element in photoVotes)
            {
                returnedValue.Add(new PhotoVoteDto
                {
                    PhotoId = element.PhotoId,
                    UserId = element.UserId,
                    Rating = element.Rating
                });
            }

            return returnedValue;

            //throw new NotImplementedException();
        }

        public bool IsPhotoOwner(string userId, int photoId)
        {
            var user = _photoAlbumContext.Users.Find(userId);
            var photo = _photoAlbumContext.Photos.Find(photoId);

            return (photo.User == user) ? true : false;

            //throw new NotImplementedException();
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
