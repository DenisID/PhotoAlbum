using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAlbum.Client.Model.Entities
{
    public class PhotoContent
    {
        [Key]
        [ForeignKey("Photo")]
        public int Id { get; set; }
        public byte[] Image { get; set; }
        public string ImageMimeType { get; set; }

        public virtual Photo Photo { get; set; }
    }
}
