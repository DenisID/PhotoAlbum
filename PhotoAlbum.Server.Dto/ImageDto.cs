using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAlbum.Server.Dto
{
    public class ImageDto
    {
        [Required]
        public byte[] Image { get; set; }

        [Required]
        public string ImageMimeType { get; set; }
    }
}
