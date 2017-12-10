using AutoMapper;
using Microsoft.AspNet.Identity;
using PhotoAlbum.Common.ErrorCodes;
using PhotoAlbum.Common.Exceptions;
using PhotoAlbum.Server.Dto;
using PhotoAlbum.Server.Model.Data;
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
        private PhotoAlbumContext _photoAlbumContext = new PhotoAlbumContext();

        public CustomUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }

        public List<UserNameDto> GetAllUserNames()
        {
            List<UserNameDto> userNamesDto = new List<UserNameDto>();

            var usersFromDb = _photoAlbumContext.Users;

            if(usersFromDb == null)
            {
                throw new UserNotFoundException(ErrorCodes.UserNotFound);
            }

            foreach(var user in usersFromDb)
            {
                userNamesDto.Add(Mapper.Map<UserNameDto>(user));
            }

            return userNamesDto;
        }
    }
}
