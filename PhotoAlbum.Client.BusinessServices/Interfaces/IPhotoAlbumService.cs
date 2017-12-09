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
        Task Test();

        Task CreatePhotoAsync(CreatePhotoDto createPhotoDto, string token);
        Task<List<PhotoDto>> GetAllPhotosAsync();
        Task<ImageDto> GetImageByIdAsync(int imageId);
        Task<HttpStatusCode> DeletePhotoByIdAsync(int photoId, string token);
        Task<HttpStatusCode> EditPhotoAsync(EditPhotoDto editPhotoDto, string token);
        Task<EditPhotoDto> GetEditPhotoByIdAsync(int editPhotoId, string token);
        Task<PhotoRatingDto> GetPhotoRatingAsync(int photoId);
        Task<List<PhotoDto>> GetPhotosAsync(PagingParametersDto paginParametersDto);
        Task<List<UserVoteDto>> GetUserVotesAsync(string token, int? photoId = null);
        //Task<HttpStatusCode> RegisterUser(RegisterUserDto registerUserDto);
    }
}
