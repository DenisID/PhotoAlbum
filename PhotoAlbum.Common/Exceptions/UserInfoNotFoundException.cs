using System;

namespace PhotoAlbum.Common.Exceptions
{
    public class UserInfoNotFoundException : PhotoAlbumException
    {
        public UserInfoNotFoundException()
        {
        }

        public UserInfoNotFoundException(string message) : base(message)
        {
        }

        public UserInfoNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
