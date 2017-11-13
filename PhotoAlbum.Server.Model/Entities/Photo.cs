using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAlbum.Server.Model.Entities
{
    public class Photo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }

        public virtual PhotoContent PhotoContent { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<PhotoVote> Votes { get; set; }

        public Photo()
        {
            Votes = new List<PhotoVote>();
        }
    }
}
