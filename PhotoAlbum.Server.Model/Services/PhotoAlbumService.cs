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
    class PhotoAlbumService : IPhotoAlbumService
    {
        private PhotoAlbumContext _photoAlbumContext = new PhotoAlbumContext();

        public void AddPhoto(AddPhotoDto addPhotoDto)
        {
            var photo = new Photo();
            photo.CreationDate = DateTime.Now;
            photo.Title = addPhotoDto.Title;
            photo.Description = addPhotoDto.Description;

            _photoAlbumContext.Photos.Add(photo);
            _photoAlbumContext.SaveChanges();

            var photoContent = new PhotoContent();
            photoContent.Id = photo.Id;
            photoContent.Image = addPhotoDto.Image;
            photoContent.ImageMimeType = addPhotoDto.ImageMimeType;

            _photoAlbumContext.PhotoContents.Add(photoContent);
            _photoAlbumContext.SaveChanges();
        }

        public List<Photo> GetAllPhotos()
        {
            return _photoAlbumContext.Photos.ToList();
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
