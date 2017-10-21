using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAlbum.Client.Model.Entities
{
    class PhotoVote : BaseEntity
    {
        public int UserId { get; set; }
        public int PhotoId { get; set; }

        public int Vote { get; set; }

        public virtual User User { get; set; }
        public virtual Photo Photo { get; set; }
    }
}
