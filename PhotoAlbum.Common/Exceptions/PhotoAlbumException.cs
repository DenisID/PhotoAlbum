using System;

namespace PhotoAlbum.Common.Exceptions
{
    public class PhotoAlbumException : ApplicationException
    {
        public PhotoAlbumException()
        {
        }

        public PhotoAlbumException(string message) 
            : base(message)
        {
        }

        public PhotoAlbumException(string message, Exception innerException) 
            : base(message, innerException)
        {
        }
    }
}
