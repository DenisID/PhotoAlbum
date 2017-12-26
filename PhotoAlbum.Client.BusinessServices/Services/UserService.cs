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
using System.Web;

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
            apiResponse.EnsureSuccessStatusCode();

            var responseContent = await apiResponse.Content.ReadAsAsync<WebApiResponseDto<RegisterUserResultDto>>();
            responseContent.ErrorMessage.TryThrowPhotoAlbumException();
            
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
            var requerstToApi = new HttpRequestMessage(HttpMethod.Post, urn) { Content = new FormUrlEncodedContent(dict) };
            var apiResponse = await _httpClient.SendAsync(requerstToApi);

            var token = await apiResponse.Content.ReadAsAsync<TokenDto>();

            return token;
        }

        public List<UserNameDto> GetAllUserNamesAsync()
        {
            List<UserNameDto> userNames = null;
            
            var urn = _uriConstantsService.GetAllUserNames;
            HttpResponseMessage apiResponse = _httpClient.GetAsync(urn).Result;
            apiResponse.EnsureSuccessStatusCode();

            var responseContent = apiResponse.Content.ReadAsAsync<WebApiResponseDto<List<UserNameDto>>>().Result;
            responseContent.ErrorMessage.TryThrowPhotoAlbumException();

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
            apiResponse.EnsureSuccessStatusCode();

            var responseContent = await apiResponse.Content.ReadAsAsync<WebApiResponseDto<EditUserProfileDto>>();
            responseContent.ErrorMessage.TryThrowPhotoAlbumException();

            dto = responseContent.Result;

            return dto;
        }

        public async Task<HttpStatusCode> EditUserProfileAsync(EditUserProfileDto dto, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var urn = _uriConstantsService.EditUserProfile;
            HttpResponseMessage apiResponse = await _httpClient.PostAsJsonAsync(urn, dto);
            _httpClient.DefaultRequestHeaders.Authorization = null;
            apiResponse.EnsureSuccessStatusCode();

            var responseContent = await apiResponse.Content.ReadAsAsync<WebApiResponseDto<int>>();
            responseContent.ErrorMessage.TryThrowPhotoAlbumException();

            return apiResponse.StatusCode;
        }

        public async Task<HttpStatusCode> ChangePasswordAsync(ChangePasswordDto dto, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var urn = _uriConstantsService.ChangePassword;
            HttpResponseMessage apiResponse = await _httpClient.PostAsJsonAsync(urn, dto);
            _httpClient.DefaultRequestHeaders.Authorization = null;
            apiResponse.EnsureSuccessStatusCode();

            var responseContent = await apiResponse.Content.ReadAsAsync<WebApiResponseDto<int>>();
            responseContent.ErrorMessage.TryThrowPhotoAlbumException();
            
            return apiResponse.StatusCode;
        }

        public async Task<UserFullNameDto> GetUserFullNameAsync(string userName)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);

            query["userName"] = userName;

            var queryString = query.ToString();

            var urn = _uriConstantsService.GetUserFullName + queryString;
            HttpResponseMessage apiResponse = _httpClient.GetAsync(urn).Result;
            apiResponse.EnsureSuccessStatusCode();

            var responseContent = apiResponse.Content.ReadAsAsync<WebApiResponseDto<UserFullNameDto>>().Result;
            responseContent.ErrorMessage.TryThrowPhotoAlbumException();

            var userFullName = responseContent.Result;

            return userFullName;
        }
    }
}
