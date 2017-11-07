using Microsoft.AspNet.Identity;
using PhotoAlbum.Server.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAlbum.Server.Model.Managers
{
    public class CustomUserManager : UserManager<ApplicationUser>
    {
        public CustomUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }
    }
}
