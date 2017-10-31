using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAlbum.Server.Model.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Photo> PhotoDescriptions { get; set; }
        public virtual ICollection<PhotoVote> PhotoVotes { get; set; }

        public User()
        {
            PhotoDescriptions = new List<Photo>();
            PhotoVotes = new List<PhotoVote>();
        }
    }
}
