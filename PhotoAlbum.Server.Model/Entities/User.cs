using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAlbum.Server.Model.Entities
{
    class User : BaseEntity
    {
        public string Login { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public virtual ICollection<PhotoDescription> PhotoDescriptions { get; set; }
        public virtual ICollection<PhotoVote> PhotoVote { get; set; }

        public User()
        {
            PhotoDescriptions = new List<PhotoDescription>();
            PhotoVote = new List<PhotoVote>();
        }
    }
}
