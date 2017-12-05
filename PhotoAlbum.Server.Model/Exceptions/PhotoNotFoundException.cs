using PhotoAlbum.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAlbum.Server.Model.Exceptions
{
    class PhotoNotFoundException : PhotoAlbumException
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

        public static PhotoNotFoundException CreateException(int photoId)
        {
            string message = $"No Photo found with ID = {photoId}";
            return new PhotoNotFoundException(message);
        }
    }
}
