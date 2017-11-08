using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoAlbum.Client.Models
{
    public class CreatePhotoViewModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public HttpPostedFileBase Image { get; set; }
    }
}