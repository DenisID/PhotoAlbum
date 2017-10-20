using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAlbum.Server.Model.Entities
{
    class PhotoDescription : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }

        public virtual Photo Photo { get; set; }
        public virtual ICollection<PhotoVote> Votes { get; set; }

        public PhotoDescription()
        {
            Votes = new List<PhotoVote>();
        }

    }
}
