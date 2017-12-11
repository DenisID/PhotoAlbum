using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAlbum.Common.Exceptions
{
    public class UserIsNotAuthorizedException : PhotoAlbumException
    {
        public UserIsNotAuthorizedException()
        {
        }

        public UserIsNotAuthorizedException(string message) : base(message)
        {
        }

        public UserIsNotAuthorizedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
