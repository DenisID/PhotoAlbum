using PhotoAlbum.Server.Dto;
using PhotoAlbum.Server.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAlbum.Server.Model.Interfaces
{
    public interface IPhotoAlbumService
    {
        int CreatePhoto(CreatePhotoDto createPhotoDto);
        //void AddPhoto(AddPhotoDto addPhotoDto);
        List<Photo> GetAllPhotos();
        Photo GetPhotoById(int photoId);
        void DeletePhoto(int photoId);
    }
}
