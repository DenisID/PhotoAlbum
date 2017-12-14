using System;

namespace PhotoAlbum.Common.Exceptions
{
    public class NotEnoughRightsException : PhotoAlbumException
    {
        public NotEnoughRightsException()
        {
        }

        public NotEnoughRightsException(string message) 
            : base(message)
        {
        }

        public NotEnoughRightsException(string message, Exception innerException) 
            : base(message, innerException)
        {
        }
    }
}