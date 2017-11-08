using AutoMapper;
using PhotoAlbum.Client.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoAlbum.Client.App_Start
{
    public class AutoMapperConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<DtoToViewModelMappingProfile>();
                x.AddProfile<ViewModelToDtoMappingProfile>();
            });
        }
    }
}