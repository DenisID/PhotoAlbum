using PhotoAlbum.Client.BusinessServices.Interfaces;
using PhotoAlbum.Client.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace PhotoAlbum.Client.Filters
{
    public class UserNameConstraint : IRouteConstraint
    {
        private readonly IUserService _userService;

        public UserNameConstraint(IUserService userService)
        {
            _userService = userService;
        }

        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            List<UserNameDto> users = _userService.GetAllUserNamesAsync();
            
            var username = values["username"].ToString().ToLower();

            return users.Any(x => x.UserName.ToLower() == username);
        }
    }
}