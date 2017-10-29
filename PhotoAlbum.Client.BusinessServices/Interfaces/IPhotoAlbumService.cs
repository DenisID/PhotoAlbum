using PhotoAlbum.Client.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAlbum.Client.BusinessServices.Interfaces
{
    public interface IPhotoAlbumService
    {
        Task<HttpResponseMessage> Test();

        Task<Uri> CreatePhoto(CreatePhotoDto createPhotoDto);
    }
}
