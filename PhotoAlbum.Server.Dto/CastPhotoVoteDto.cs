using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAlbum.Server.Dto
{
    public class CastPhotoVoteDto
    {
        public int PhotoId { get; set; }
        public int Rating { get; set; }
    }
}
