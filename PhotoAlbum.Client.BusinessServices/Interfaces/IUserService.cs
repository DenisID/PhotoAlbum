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
        Task<HttpStatusCode> RegisterUser(RegisterUserDto registerUserDto);
    }
}
