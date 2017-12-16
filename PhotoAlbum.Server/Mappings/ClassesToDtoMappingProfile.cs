using AutoMapper;
using Microsoft.AspNet.Identity;
using PhotoAlbum.Server.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoAlbum.Server.Mappings
{
    public class ClassesToDtoMappingProfile : Profile
    {
        public override string ProfileName
    {
        get { return "ClassesToDtoMappings"; }
    }

    public ClassesToDtoMappingProfile()
    {
            CreateMap<IdentityResult, RegisterUserResultDto>();
    }
}
}