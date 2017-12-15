using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAlbum.Server.Dto
{
    public class PhotoVoteDto
    {
        public string UserId { get; set; }
        
        public int PhotoId { get; set; }
        
        public int Rating { get; set; }
    }
}
