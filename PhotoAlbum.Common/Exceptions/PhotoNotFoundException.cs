using System;

namespace PhotoAlbum.Common.Exceptions
{
    public class PhotoNotFoundException : PhotoAlbumException
    {
        public PhotoNotFoundException()
        {
        }

        public PhotoNotFoundException(string message) 
            : base(message)
        {
        }

        public PhotoNotFoundException(string message, Exception innerException) 
            : base(message, innerException)
        {
        }
    }
}
