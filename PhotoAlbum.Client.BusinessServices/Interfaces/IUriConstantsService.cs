using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAlbum.Client.BusinessServices.Interfaces
{
    public interface IUriConstantsService
    {
        string CreatePhoto { get; }
        string GetAllPhotos { get; }
        string GetPhotos { get; }
        string GetUserPhotos { get; }
        string GetImageById { get; }
        string DeletePhotoById { get; }
        string EditPhoto { get; }
        string GetEditPhotoById { get; }
        string GetPhotoRating { get; }
        string GetUserVotes { get; }
        string CastPhotoVote { get; }

        string RegisterUser { get; }
        string GetToken { get; }
        string GetAllUserNames { get; }
        string GetUserProfile { get; }
        string EditUserProfile { get; }
        string ChangePassword { get; }
        string GetUserFullName { get; }
    }
}
