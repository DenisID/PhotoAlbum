using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoAlbum.Client.Models
{
    public class PhotoViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        //public byte[] Image { get; set; }
        //public string ImageMimeType { get; set; }
    }

    public class CreatePhotoViewModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public HttpPostedFileBase Image { get; set; }
    }

    public class EditPhotoViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}