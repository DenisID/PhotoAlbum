using PhotoAlbum.Server.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAlbum.Server.Model.Data
{
    class PhotoAlbumContext : DbContext
    {
        public PhotoAlbumContext()
            : base("name=DefaultConnection")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<PhotoAlbumContext>());
        }

        public DbSet<Photo> Photos { get; set; }
        public DbSet<PhotoContent> PhotoContents { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<PhotoVote> PhotoVotes { get; set; }
    }
}
