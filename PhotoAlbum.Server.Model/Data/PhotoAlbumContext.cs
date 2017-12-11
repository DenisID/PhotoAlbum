using Microsoft.AspNet.Identity.EntityFramework;
using PhotoAlbum.Server.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAlbum.Server.Model.Data
{
    public class PhotoAlbumContext : IdentityDbContext<ApplicationUser>
    {
        public PhotoAlbumContext()
            : base("name=DefaultConnection")
        {
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<PhotoAlbumContext>());
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<PhotoAlbumContext, Migrations.Configuration>("DefaultConnection"));
        }

        public DbSet<Photo> Photos { get; set; }
        public DbSet<PhotoContent> PhotoContents { get; set; }
        public DbSet<PhotoVote> PhotoVotes { get; set; }
        public DbSet<UserInfo> UserInfoes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
        }

        public static PhotoAlbumContext Create()
        {
            return new PhotoAlbumContext();
        }
    }
}
