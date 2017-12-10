using AutoMapper;
using Microsoft.AspNet.Identity;
using PhotoAlbum.Common.ErrorCodes;
using PhotoAlbum.Common.Exceptions;
using PhotoAlbum.Server.Dto;
using PhotoAlbum.Server.Model.Data;
using PhotoAlbum.Server.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public void ChangeUserProfile(ChangeUserProfileDto dto, string userId)
        {
            var user = _photoAlbumContext.Users.Find(userId);
            if (user == null)
            {
                throw new UserNotFoundException(ErrorCodes.UserNotFound);
            }

            var userInfo = _photoAlbumContext.UserInfoes.FirstOrDefault(x => x.UserId == userId);
            if (userInfo == null)
            {
                throw new UserInfoNotFoundException(ErrorCodes.UserInfoNotFound);
            }

            user.Email = dto.Email;
            _photoAlbumContext.Entry(user).State = EntityState.Modified;

            userInfo.FirstName = dto.FirstName;
            userInfo.LastName = dto.LastName;
            _photoAlbumContext.Entry(userInfo).State = EntityState.Modified;

            _photoAlbumContext.SaveChanges();
        }

        public ChangeUserProfileDto GetUserProfile(string userId)
        {
            var user = _photoAlbumContext.Users.Find(userId);
            if (user == null)
            {
                throw new UserNotFoundException(ErrorCodes.UserNotFound);
            }

            var dto = new ChangeUserProfileDto()
            {
                Email = user.Email,
                FirstName = user.UserInfo.FirstName,
                LastName = user.UserInfo.LastName
            };

            return dto;
        }
    }
}
