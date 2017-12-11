using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
