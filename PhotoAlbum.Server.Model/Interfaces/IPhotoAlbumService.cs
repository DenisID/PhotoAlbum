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
        List<PhotoDto> GetAllPhotos();
        List<PhotoDto> GetPhotos(PagingParametersDto pagingParameter);
        List<PhotoDto> GetUserPhotos(PagingParametersDto pagingParameter, string userName);
        ImageDto GetImageById(int imageId);
        void DeletePhotoById(int photoId);
        void EditPhoto(EditPhotoDto editPhotoDto);
        EditPhotoDto GetEditPhotoById(int editPhotoId);
        void CastPhotoVote(PhotoVoteDto castPhotoVoteDto);
        List<PhotoVoteDto> GetUserVotes(string userId, int? photoId = null);
        int GetPhotosCount();
        PhotoRatingDto GetPhotoRating(int photoId);

        bool IsPhotoOwner(string userId, int photoId);
        void CreateUserInfo(CreateUserInfoDto createUserInfoDto);
        void ChangeUserProfile(ChangeUserProfileDto dto);
    }
}
