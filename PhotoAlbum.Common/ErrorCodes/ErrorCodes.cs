using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAlbum.Common.ErrorCodes
{
    public class ErrorCodes
    {
        public static string PhotoNotFound { get; } = "PhotoNotFound";
        public static string NoPhotosInDatabase { get; } = "NoPhotosInDatabase";
        public static string UserNotFound { get; } = "UserNotFound";
    }
}
