using PhotoAlbum.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAlbum.Server.Model.Exceptions
{
    class UserNotFoundException : PhotoAlbumException
    {
        public UserNotFoundException()
        {
        }

        public UserNotFoundException(string message) : base(message)
        {
        }

        public UserNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public static UserNotFoundException CreateException(string userId)
        {
            string message = $"No User found with ID = {userId}";
            return new UserNotFoundException(message);
        }
    }
}
