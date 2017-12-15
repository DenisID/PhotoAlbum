using DelegateDecompiler;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
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
        
        [Computed]
        public double Rating
        {
            get
            {
                return Votes.Average(x => x.Rating);
            }
        }

        public virtual PhotoContent PhotoContent { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<PhotoVote> Votes { get; set; }

        public Photo()
        {
            Votes = new List<PhotoVote>();
        }
    }
}
