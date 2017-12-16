using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAlbum.Server.Dto
{
    public class RegisterUserResultDto
    {
        public IEnumerable<string> Errors { get; set; }
        public bool Successeded { get; set; }
    }
}
