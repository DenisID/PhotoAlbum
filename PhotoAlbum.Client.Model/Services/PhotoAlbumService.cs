using PhotoAlbum.Client.Model.Data;
using PhotoAlbum.Client.Model.Entities;
using PhotoAlbum.Client.Model.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAlbum.Client.Model.Services
{
    public class PhotoAlbumService : IPhotoAlbumService
    {
        private PhotoAlbumContext _dbContext = new PhotoAlbumContext();
        
        public void AddPhoto(byte[] image)
        {
            var photo = new Photo();
            photo.CreationDate = DateTime.Now;
            _dbContext.Photos.Add(photo);
            _dbContext.SaveChanges();

            var photoContent = new PhotoContent();
            photoContent.Id = photo.Id;
            photoContent.Image = image;
            _dbContext.PhotoContents.Add(photoContent);
            _dbContext.SaveChanges();
        }

        public List<PhotoContent> GetAllPhoto()
        {
            return _dbContext.PhotoContents.ToList();
        }
    }
}
