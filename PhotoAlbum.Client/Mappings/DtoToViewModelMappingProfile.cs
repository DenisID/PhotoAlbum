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
        }
    }
}