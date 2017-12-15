﻿using PhotoAlbum.Client.BusinessServices.Helpers;
using PhotoAlbum.Client.BusinessServices.Interfaces;
using PhotoAlbum.Client.Dto;
using PhotoAlbum.Common.ErrorCodes;
using PhotoAlbum.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
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
            string webApiUri = ConfigurationManager.AppSettings["WebApiUri"];
            _httpClient.BaseAddress = new Uri(webApiUri);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        
        public async Task CreatePhotoAsync(CreatePhotoDto createPhotoDto, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage apiResponse = await _httpClient.PostAsJsonAsync("api/photo", createPhotoDto);
            _httpClient.DefaultRequestHeaders.Authorization = null;

            var responseContent = await apiResponse.Content.ReadAsAsync<WebApiResponseDto<int>>();

            // Exceptions check
            responseContent.ErrorMessage.TryThrowPhotoAlbumException();
            apiResponse.EnsureSuccessStatusCode();
        }

        public async Task<List<PhotoDto>> GetAllPhotosAsync()
        {
            List<PhotoDto> photos = null;
            HttpResponseMessage apiResponse = await _httpClient.GetAsync("api/photo");
            
            var responseContent = await apiResponse.Content.ReadAsAsync<WebApiResponseDto<List<PhotoDto>>>();

            // Exceptions check
            responseContent.ErrorMessage.TryThrowPhotoAlbumException();
            apiResponse.EnsureSuccessStatusCode();

            photos = responseContent.Result;

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
            
            var responseContent = await apiResponse.Content.ReadAsAsync<WebApiResponseDto<List<PhotoDto>>>();

            // Exceptions check
            responseContent.ErrorMessage.TryThrowPhotoAlbumException();
            apiResponse.EnsureSuccessStatusCode();

            photos = responseContent.Result;

            return photos;
        }

        public async Task<List<PhotoDto>> GetUserPhotosAsync(PagingParametersDto paginParametersDto, string userName)
        {
            List<PhotoDto> photos = null;

            var query = HttpUtility.ParseQueryString(string.Empty);

            query["PageNumber"] = paginParametersDto.PageNumber.ToString();
            query["PageSize"] = paginParametersDto.PageSize.ToString();
            query["Sorting"] = paginParametersDto.Sorting.ToString();
            query["userName"] = userName;

            var queryString = query.ToString();
            
            HttpResponseMessage apiResponse = await _httpClient.GetAsync($"api/userphotos?" + queryString);

            var responseContent = await apiResponse.Content.ReadAsAsync<WebApiResponseDto<List<PhotoDto>>>();

            // Exceptions check
            responseContent.ErrorMessage.TryThrowPhotoAlbumException();
            apiResponse.EnsureSuccessStatusCode();

            photos = responseContent.Result;

            return photos;
        }

        public async Task<ImageDto> GetImageByIdAsync(int imageId, string eTag)
        {
            ImageDto image = null;

            _httpClient.DefaultRequestHeaders.IfNoneMatch.Add(new EntityTagHeaderValue("\"" + eTag + "\"", true));

            HttpResponseMessage apiResponse = await _httpClient.GetAsync($"api/photo/image/{imageId}");
            _httpClient.DefaultRequestHeaders.IfNoneMatch.Clear();

            if(apiResponse.StatusCode == HttpStatusCode.NotModified)
            {
                return new ImageDto() { IsNotModified = true };
            }
            
            var responseContent = await apiResponse.Content.ReadAsAsync<WebApiResponseDto<ImageDto>>();

            // Exceptions check
            responseContent.ErrorMessage.TryThrowPhotoAlbumException();
            apiResponse.EnsureSuccessStatusCode();

            image = responseContent.Result;

            return image;
        }

        public async Task<HttpStatusCode> DeletePhotoByIdAsync(int photoId, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage apiResponse = await _httpClient.DeleteAsync($"api/photo/{photoId}");
            _httpClient.DefaultRequestHeaders.Authorization = null;

            // Exceptions check
            var responseContent = await apiResponse.Content.ReadAsAsync<WebApiResponseDto<int>>();
            responseContent.ErrorMessage.TryThrowPhotoAlbumException();
            apiResponse.EnsureSuccessStatusCode();

            return apiResponse.StatusCode;
        }

        public async Task<HttpStatusCode> EditPhotoAsync(EditPhotoDto editPhotoDto, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage apiResponse = await _httpClient.PutAsJsonAsync($"api/photo/editphoto", editPhotoDto);
            _httpClient.DefaultRequestHeaders.Authorization = null;

            // Exceptions check
            var responseContent = await apiResponse.Content.ReadAsAsync<WebApiResponseDto<int>>();
            responseContent.ErrorMessage.TryThrowPhotoAlbumException();
            apiResponse.EnsureSuccessStatusCode();

            return apiResponse.StatusCode;
        }

        public async Task<EditPhotoDto> GetEditPhotoByIdAsync(int editPhotoId, string token)
        {
            EditPhotoDto editoPhotoDto = null;

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage apiResponse = await _httpClient.GetAsync($"api/photo/editphoto/{editPhotoId}");
            _httpClient.DefaultRequestHeaders.Authorization = null;
            
            var responseContent = await apiResponse.Content.ReadAsAsync<WebApiResponseDto<EditPhotoDto>>();

            // Exceptions check
            responseContent.ErrorMessage.TryThrowPhotoAlbumException();
            apiResponse.EnsureSuccessStatusCode();

            editoPhotoDto = responseContent.Result;

            return editoPhotoDto;
        }

        public async Task<PhotoRatingDto> GetPhotoRatingAsync(int photoId)
        {
            PhotoRatingDto photoRatingDto = null;

            HttpResponseMessage apiResponse = await _httpClient.GetAsync($"api/photo/rating/{photoId}");

            var responseContent = await apiResponse.Content.ReadAsAsync<WebApiResponseDto<PhotoRatingDto>>();

            // Exceptions check
            responseContent.ErrorMessage.TryThrowPhotoAlbumException();
            apiResponse.EnsureSuccessStatusCode();

            photoRatingDto = responseContent.Result;

            return photoRatingDto;
        }

        public async Task<List<UserVoteDto>> GetUserVotesAsync(string token, int? photoId = null)
        {
            List<UserVoteDto> userVotesDto = null;

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage apiResponse = null;
            if (photoId == null)
            {
                apiResponse = await _httpClient.GetAsync($"api/photo/vote");
            }
            else
            {
                apiResponse = await _httpClient.GetAsync($"api/photo/vote/{photoId}");
            }
            
            _httpClient.DefaultRequestHeaders.Authorization = null;
            
            var responseContent = await apiResponse.Content.ReadAsAsync<WebApiResponseDto<List<UserVoteDto>>>();

            // Exceptions check
            responseContent.ErrorMessage.TryThrowPhotoAlbumException();
            apiResponse.EnsureSuccessStatusCode();

            userVotesDto = responseContent.Result;

            return userVotesDto;
        }

        public async Task<HttpStatusCode> CastPhotoVoteAsync(PhotoVoteDto photoVoteDto, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage apiResponse = await _httpClient.PostAsJsonAsync($"api/photo/vote", photoVoteDto);
            _httpClient.DefaultRequestHeaders.Authorization = null;

            // Exceptions check
            var responseContent = await apiResponse.Content.ReadAsAsync<WebApiResponseDto<int>>();
            responseContent.ErrorMessage.TryThrowPhotoAlbumException();
            apiResponse.EnsureSuccessStatusCode();

            return apiResponse.StatusCode;
        }
    }
}
