using AutoMapper;
using PhotoAlbum.Client.Dto;
using PhotoAlbum.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoAlbum.Client.Mappings
{
    public class DtoToViewModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "DtoToViewModelMappings"; }
        }

        public DtoToViewModelMappingProfile()
        {
            CreateMap<EditPhotoDto, EditPhotoViewModel>();
            CreateMap<EditUserProfileDto, EditUserProfileViewModel>();
            CreateMap<UserFullNameDto, UserPageViewModel>()
                .ForMember(d => d.UserFirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(d => d.UserLastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(d => d.SortPhoto, opt => opt.Ignore())
                .ForMember(d => d.UserName, opt => opt.Ignore());
        }
    }
}