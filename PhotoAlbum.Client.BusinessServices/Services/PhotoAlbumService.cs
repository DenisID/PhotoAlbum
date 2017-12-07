using PhotoAlbum.Client.BusinessServices.Interfaces;
using PhotoAlbum.Client.Dto;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

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
            //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "Your Oauth token");
        }

        public async Task CreatePhotoAsync(CreatePhotoDto createPhotoDto, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("api/photo", createPhotoDto);
            response.EnsureSuccessStatusCode();
        }

        public async Task<List<PhotoDto>> GetAllPhotosAsync()
        {
            List<PhotoDto> photos = null;
            HttpResponseMessage apiResponse = await _httpClient.GetAsync("api/photo");
            if (apiResponse.IsSuccessStatusCode)
            {
                var responseContent = await apiResponse.Content.ReadAsAsync<WebApiResponseDto<List<PhotoDto>>>();
                photos = responseContent.Result;
            }

            return photos;
        }

        public async Task<List<PhotoDto>> GetPhotosAsync(PagingParametersDto paginParametersDto)
        {
            List<PhotoDto> photos = null;

            var query = HttpUtility.ParseQueryString(string.Empty);

            query["PageNumber"] = paginParametersDto.PageNumber.ToString();
            query["PageSize"] = paginParametersDto.PageSize.ToString();
            query["Sorting"] = paginParametersDto.Sorting.ToString();

            var queryString = query.ToString();

            HttpResponseMessage apiResponse = await _httpClient.GetAsync($"api/photo?" + queryString);
            if (apiResponse.IsSuccessStatusCode)
            {
                var responseContent = await apiResponse.Content.ReadAsAsync<WebApiResponseDto<List<PhotoDto>>>();
                photos = responseContent.Result;
            }

            return photos;
        }

        public async Task<ImageDto> GetImageByIdAsync(int imageId)
        {
            ImageDto image = null;

            HttpResponseMessage apiResponse = await _httpClient.GetAsync($"api/photo/image/{imageId}");
            if (apiResponse.IsSuccessStatusCode)
            {
                var responseContent = await apiResponse.Content.ReadAsAsync<WebApiResponseDto<ImageDto>>();
                image = responseContent.Result;
            }

            return image;
        }

        public async Task<HttpStatusCode> DeletePhotoByIdAsync(int photoId, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await _httpClient.DeleteAsync($"api/photo/{photoId}");
            _httpClient.DefaultRequestHeaders.Authorization = null;

            return response.StatusCode;
        }

        public async Task<HttpStatusCode> EditPhotoAsync(EditPhotoDto editPhotoDto, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"api/photo/editphoto", editPhotoDto);
            _httpClient.DefaultRequestHeaders.Authorization = null;

            return response.StatusCode;
        }

        public async Task<EditPhotoDto> GetEditPhotoByIdAsync(int editPhotoId, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            EditPhotoDto editoPhotoDto = null;
            HttpResponseMessage apiResponse = await _httpClient.GetAsync($"api/photo/editphoto/{editPhotoId}");
            if (apiResponse.IsSuccessStatusCode)
            {
                var responseContent = await apiResponse.Content.ReadAsAsync<WebApiResponseDto<EditPhotoDto>>();
                editoPhotoDto = responseContent.Result;
            }

            return editoPhotoDto;
        }

        public async Task<PhotoRatingDto> GetPhotoRatingAsync(int photoId)
        {
            PhotoRatingDto photoRatingDto = null;
            HttpResponseMessage apiResponse = await _httpClient.GetAsync($"api/photo/vote/{photoId}");
            if(apiResponse.IsSuccessStatusCode)
            {
                var responseContent = await apiResponse.Content.ReadAsAsync<WebApiResponseDto<PhotoRatingDto>>();
                photoRatingDto = responseContent.Result;
            }

            return photoRatingDto;
        }

        //public async Task<HttpStatusCode> RegisterUser(RegisterUserDto registerUserDto)
        //{
        //    HttpResponseMessage response = await _httpClient.PostAsJsonAsync("api/Account/Register", registerUserDto);
        //    return response.StatusCode;
        //}
    }
}
