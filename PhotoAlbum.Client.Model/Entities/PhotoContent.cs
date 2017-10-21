using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAlbum.Client.Model.Entities
{
    class PhotoContent : BaseEntity
    {
        public byte[] Image { get; set; }
        public string ImageMimeType { get; set; }

        public virtual Photo Photo { get; set; }
    }
}
