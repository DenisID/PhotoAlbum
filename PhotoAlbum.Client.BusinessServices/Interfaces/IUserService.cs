using PhotoAlbum.Client.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAlbum.Client.BusinessServices.Interfaces
{
    public interface IUserService
    {
        Task<RegisterUserResultDto> RegisterUser(RegisterUserDto registerUserDto);
        Task<TokenDto> GetTokenAsync(GetTokenDto getTokenDto);
        List<UserNameDto> GetAllUserNamesAsync();
        Task<EditUserProfileDto> GetUserProfileAsync(string token);
        Task<HttpStatusCode> EditUserProfileAsync(EditUserProfileDto dto, string token);
        Task<HttpStatusCode> ChangePasswordAsync(ChangePasswordDto dto, string token);
    }
}
