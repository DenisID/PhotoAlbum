using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAlbum.Server.Model.Entities
{
    class Photo : BaseEntity
    {
        public byte[] Image { get; set; }
        public string ImageMimeType { get; set; }

        public virtual PhotoDescription PhotoDescription { get; set; }
    }
}
