﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAlbum.Client.Dto
{
    public class CreatePhotoDto
    {
        public byte[] Image { get; set; }
        public string ImageMimeType { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
