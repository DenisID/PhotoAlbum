using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAlbum.Client.Dto
{
    public class PhotoVoteDto
    {
        public int PhotoId { get; set; }
        public int Rating { get; set; }
    }
}
