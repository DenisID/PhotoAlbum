﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAlbum.Client.Dto
{
    public class PhotoDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public string OwnerName { get; set; }
        public double Rating { get; set; }
        //public byte[] Image { get; set; }
        //public string ImageMimeType { get; set; }
    }
}
