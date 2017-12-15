using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAlbum.Server.Dto
{
    public class CastPhotoVoteDto
    {
        [Required]
        public int PhotoId { get; set; }

        [Required]
        [Range(0, 5)]
        public int Rating { get; set; }
    }
}
