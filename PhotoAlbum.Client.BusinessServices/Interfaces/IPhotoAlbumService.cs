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
        Task<Uri> CreatePhoto(CreatePhotoDto createPhotoDto, string token);
        Task<List<PhotoDto>> GetAllPhotos();
        Task<ImageDto> GetImageById(int imageId);
        Task<HttpStatusCode> DeletePhotoById(int photoId, string token);
        Task<HttpStatusCode> EditPhoto(EditPhotoDto editPhotoDto, string token);
        Task<EditPhotoDto> GetEditPhotoById(int editPhotoId, string token);
        Task<PhotoRatingDto> GetPhotoRatingAsync(int photoId);
        //Task<HttpStatusCode> RegisterUser(RegisterUserDto registerUserDto);
    }
}
