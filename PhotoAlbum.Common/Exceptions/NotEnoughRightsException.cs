using PhotoAlbum.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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