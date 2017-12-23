using PhotoAlbum.Common.ErrorCodes;
using PhotoAlbum.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAlbum.Client.BusinessServices.Helpers
{
    public static class PhotoAlbumExceptionHelper
    {
        public static void TryThrowPhotoAlbumException(this string exMessage)
        {
            switch (exMessage)
            {
                case ErrorCodes.PhotoNotFound:
                    throw new PhotoNotFoundException(exMessage);

                case ErrorCodes.NoPhotosInDatabase:
                    throw new PhotoNotFoundException(exMessage);

                case ErrorCodes.UserNotFound:
                    throw new UserNotFoundException(exMessage);

                case ErrorCodes.NotEnoughRights:
                    throw new NotEnoughRightsException(exMessage);
                    
                case ErrorCodes.UserInfoNotFound:
                    throw new UserNotFoundException(exMessage);

                case ErrorCodes.UserIsNotAuthorized:
                    throw new UserNotFoundException(exMessage);
            }
        }
    }
}
