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
            var photosFromDb = _photoAlbumContext.Photos.ToList();
            var photos = new List<PhotoDto>();
            if(photosFromDb != null)
            {
                foreach(var photoFromDb in photosFromDb)
                {
                    photos.Add(new PhotoDto
                    {
                        Id = photoFromDb.Id,
                        Title = photoFromDb.Title,
                        Description = photoFromDb.Description,
                        CreationDate = photoFromDb.CreationDate,
                        Image = photoFromDb.PhotoContent.Image,
                        ImageMimeType = photoFromDb.PhotoContent.ImageMimeType
                    });
                }
            }


            return photos;
        }

        public Photo GetPhotoById(int photoId)
        {
            if (photoId < 0)
            {
                throw new ArgumentNullException(nameof(photoId));
            }

            var photo = _photoAlbumContext.Photos.Find(photoId);
            //if (photo == null)
            //{
            //    throw PhotoNotFoundException.FromPhotoId(photoId);
            //}
            //photo.ValidateEntity();
            return photo;
        }

        public void DeletePhoto(int photoId)
        {
            var photo = _photoAlbumContext.Photos.Find(photoId);
            var photoContent = _photoAlbumContext.PhotoContents.Find(photoId);
            _photoAlbumContext.Entry(photoContent).State = EntityState.Deleted;
            _photoAlbumContext.Entry(photo).State = EntityState.Deleted;
            _photoAlbumContext.SaveChanges();
        }
    }
}
