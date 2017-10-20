using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAlbum.Server.Model.Entities
{
    class PhotoVote : BaseEntity
    {
        public int Vote { get; set; }

        public virtual ICollection<PhotoDescription> PhotoDescriptions { get; set; }
        public virtual ICollection<User> Users { get; set; }

        public PhotoVote()
        {
            PhotoDescriptions = new List<PhotoDescription>();
            Users = new List<User>();
        }
    }
}
