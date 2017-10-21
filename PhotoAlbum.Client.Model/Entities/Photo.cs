using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAlbum.Client.Model.Entities
{
    class Photo : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }

        public virtual PhotoContent PhotoContent { get; set; }
        public virtual ICollection<PhotoVote> Votes { get; set; }

        public Photo()
        {
            Votes = new List<PhotoVote>();
        }
    }
}
