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
        [Required]
        public string UserId { get; set; }

        [Required]
        public int PhotoId { get; set; }

        [Required]
        public int Rating { get; set; }
    }
}
