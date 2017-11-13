using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAlbum.Server.Model.Entities
{
    public class PhotoVote
    {
        [Key, Column(Order = 0)]
        public string UserId { get; set; }

        [Key, Column(Order = 1)]
        public int PhotoId { get; set; }

        public int Vote { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual Photo Photo { get; set; }
    }
}
