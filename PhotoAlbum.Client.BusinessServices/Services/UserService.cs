using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PhotoAlbum.Client.BusinessServices.Helpers;
using PhotoAlbum.Client.BusinessServices.Interfaces;
using PhotoAlbum.Client.Dto;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAlbum.Client.BusinessServices.Services
{
    public class UserService : IUserService
    {
        private static HttpClient _httpClient = new HttpClient();
        private IUriConstantsService _uriConstantsService;

        public UserService(IUriConstantsService uriConstantsService)
        {
            _uriConstantsService = uriConstantsService;
        }

        static UserService()
        {
            string webApiUri = ConfigurationManager.AppSettings["WebApiUri"];
            _httpClient.BaseAddress = new Uri(webApiUri);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<RegisterUserResultDto> RegisterUser(RegisterUserDto registerUserDto)
        {
            RegisterUserResultDto dto = null;
            
            var urn = _uriConstantsService.RegisterUser;
            HttpResponseMessage apiResponse = await _httpClient.PostAsJsonAsync(urn, registerUserDto);

            var responseContent = await apiResponse.Content.ReadAsAsync<WebApiResponseDto<RegisterUserResultDto>>();

            // Exceptions check
            responseContent.ErrorMessage.TryThrowPhotoAlbumException();
            apiResponse.EnsureSuccessStatusCode();

            dto = responseContent.Result;

            return dto;
        }
        
        public async Task<TokenDto> GetTokenAsync(GetTokenDto getTokenDto)
        {
            var dict = new Dictionary<string, string>();
            dict.Add("userName", getTokenDto.Login);
            dict.Add("password", getTokenDto.Password);
            dict.Add("grant_type", "password");

            var urn = _uriConstantsService.GetToken;
            var req = new HttpRequestMessage(HttpMethod.Post, urn) { Content = new FormUrlEncodedContent(dict) };
            var res = await _httpClient.SendAsync(req);
            var token = await res.Content.ReadAsAsync<TokenDto>();

            return token;
        }

        public List<UserNameDto> GetAllUserNamesAsync()
        {
            List<UserNameDto> userNames = null;

            var urn = _uriConstantsService.GetAllUserNames;
            HttpResponseMessage apiResponse = _httpClient.GetAsync(urn).Result;

            var responseContent = apiResponse.Content.ReadAsAsync<WebApiResponseDto<List<UserNameDto>>>().Result;
            
            // Exceptions check
            responseContent.ErrorMessage.TryThrowPhotoAlbumException();
            apiResponse.EnsureSuccessStatusCode();

            userNames = responseContent.Result;

            return userNames;
        }

        public async Task<EditUserProfileDto> GetUserProfileAsync(string token)
        {
            EditUserProfileDto dto = null;

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var urn = _uriConstantsService.GetUserProfile;
            HttpResponseMessage apiResponse = await _httpClient.GetAsync(urn);
            _httpClient.DefaultRequestHeaders.Authorization = null;

            var responseContent = await apiResponse.Content.ReadAsAsync<WebApiResponseDto<EditUserProfileDto>>();

            // Exceptions check
            responseContent.ErrorMessage.TryThrowPhotoAlbumException();
            apiResponse.EnsureSuccessStatusCode();

            dto = responseContent.Result;

            return dto;
        }

        public async Task<HttpStatusCode> EditUserProfileAsync(EditUserProfileDto dto, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var urn = _uriConstantsService.EditUserProfile;
            HttpResponseMessage apiResponse = await _httpClient.PostAsJsonAsync(urn, dto);
            _httpClient.DefaultRequestHeaders.Authorization = null;

            var responseContent = await apiResponse.Content.ReadAsAsync<WebApiResponseDto<int>>();

            // Exceptions check
            responseContent.ErrorMessage.TryThrowPhotoAlbumException();
            apiResponse.EnsureSuccessStatusCode();

            return apiResponse.StatusCode;
        }

        public async Task<HttpStatusCode> ChangePasswordAsync(ChangePasswordDto dto, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var urn = _uriConstantsService.ChangePassword;
            HttpResponseMessage apiResponse = await _httpClient.PostAsJsonAsync(urn, dto);
            _httpClient.DefaultRequestHeaders.Authorization = null;

            var responseContent = await apiResponse.Content.ReadAsAsync<WebApiResponseDto<int>>();

            // Exceptions check
            responseContent.ErrorMessage.TryThrowPhotoAlbumException();
            apiResponse.EnsureSuccessStatusCode();
            
            return apiResponse.StatusCode;
        }
    }
}
