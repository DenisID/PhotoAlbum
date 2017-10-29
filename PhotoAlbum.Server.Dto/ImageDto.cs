using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAlbum.Server.Dto
{
    public class ImageDto
    {
        public byte[] Image { get; set; }
        public string ImageMimeType { get; set; }
    }
}
