using PhotoAlbum.Client.BusinessServices.Interfaces;
using PhotoAlbum.Client.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAlbum.Client.BusinessServices.Services
{
    public class PhotoAlbumService : IPhotoAlbumService
    {
        private static HttpClient _httpClient = new HttpClient();

        static PhotoAlbumService()
        {
            _httpClient.BaseAddress = new Uri("http://localhost:52670/");
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        //public async Task<HttpResponseMessage> Test()
        //{
        //    HttpResponseMessage response = await _httpClient.GetAsync("Api/Photo");
        //    if (response.IsSuccessStatusCode)
        //    {
        //        //product = await response.Content.ReadAsAsync<Product>();
        //        return response;
        //    }
        //    return response;
        //}

        public async Task<Uri> CreatePhoto(CreatePhotoDto createPhotoDto)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("api/photo", createPhotoDto);
            response.EnsureSuccessStatusCode();

            // Return the URI of the created resource.
            return response.Headers.Location;
        }

        public async Task<List<PhotoDto>> GetAllPhotos()
        {
            List<PhotoDto> photos = null;
            HttpResponseMessage response = await _httpClient.GetAsync("api/photo");
            if (response.IsSuccessStatusCode)
            {
                photos = await response.Content.ReadAsAsync<List<PhotoDto>>();
            }

            return photos;
        }

        public async Task<ImageDto> GetImageById(int imageId)
        {
            ImageDto image = null;
            HttpResponseMessage response = await _httpClient.GetAsync($"api/photo/image/{imageId}");
            if (response.IsSuccessStatusCode)
            {
                image = await response.Content.ReadAsAsync<ImageDto>();
            }

            return image;
        }

        public async Task<HttpStatusCode> DeletePhotoById(int photoId)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"api/photo/{photoId}");
            return response.StatusCode;
        }

        public async Task<HttpStatusCode> EditPhoto(EditPhotoDto editPhotoDto)
        {
            HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"api/products/{editPhotoDto.Id}", editPhotoDto);
            return response.StatusCode;
        }
    }
}
