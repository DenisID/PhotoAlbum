namespace PhotoAlbum.Common.ErrorCodes
{
    public class ErrorCodes
    {
        public const string ErrorsMark = "#PAE:";

        public const string PhotoNotFound = ErrorsMark + "Photo Not Found";
        public const string NoPhotosInDatabase = ErrorsMark + "No Photos In Database";
        public const string UserNotFound = ErrorsMark + "User Not Found";
        public const string NotEnoughRights = ErrorsMark + "Not Enough Rights";
        public const string UserInfoNotFound = ErrorsMark + "User Info Not Found";
        public const string UserIsNotAuthorized = ErrorsMark + "User Is Not Authorized";
    }
}
