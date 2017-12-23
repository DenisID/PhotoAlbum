using PhotoAlbum.Common.ErrorCodes;
using Resources;

namespace PhotoAlbum.Client.Helpers
{
    public static class PhotoAlbumExMessageHelper
    {
        public static string GetLocalExString(this string exMessage)
        {
            switch (exMessage)
            {
                case ErrorCodes.PhotoNotFound:
                    return ErrorMsg.PhotoNotFound;

                case ErrorCodes.NoPhotosInDatabase:
                    return ErrorMsg.NoPhotosInDatabase;

                case ErrorCodes.UserNotFound:
                    return ErrorMsg.UserNotFound;

                case ErrorCodes.NotEnoughRights:
                    return ErrorMsg.NotEnoughRights;

                case ErrorCodes.UserInfoNotFound:
                    return ErrorMsg.UserInfoNotFound;

                case ErrorCodes.UserIsNotAuthorized:
                    return ErrorMsg.UserIsNotAuthorized;

                default:
                    return ErrorMsg.AnUnexpectedErrorOccurred;
            }
        }
    }
}