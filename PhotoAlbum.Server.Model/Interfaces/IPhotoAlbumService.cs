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
        ImageDto GetImageById(int imageId);
        void DeletePhotoById(int photoId);
        void EditPhoto(EditPhotoDto editPhotoDto);
        EditPhotoDto GetEditPhotoById(int editPhotoId);
        void CastPhotoVote(PhotoVoteDto castPhotoVoteDto);
        PhotoRatingDto GetPhotoRating(int photoId);
        List<PhotoVoteDto> GetUserVotes(string userId);

        bool IsPhotoOwner(string userId, int photoId);
        //void AddPhoto(AddPhotoDto addPhotoDto);
        //Photo GetPhotoById(int photoId);
    }
}
