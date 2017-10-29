using PhotoAlbum.Client.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAlbum.Client.BusinessServices.Interfaces
{
    public interface IPhotoAlbumService
    {
        Task<Uri> CreatePhoto(CreatePhotoDto createPhotoDto);
        Task<List<PhotoDto>> GetAllPhotos();
        Task<ImageDto> GetImageById(int imageId);
        Task<HttpStatusCode> DeletePhotoById(int photoId);
    }
}
