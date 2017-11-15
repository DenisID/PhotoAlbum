using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
    public class UserService : IUserService
    {
        private static HttpClient _httpClient = new HttpClient();

        static UserService()
        {
            _httpClient.BaseAddress = new Uri("http://localhost:52670/");
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<HttpStatusCode> RegisterUser(RegisterUserDto registerUserDto)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("api/Account/Register", registerUserDto);
            return response.StatusCode;
        }

        // TODO : mb rework
        public async Task<TokenDto> GetTokenAsync(GetTokenDto getTokenDto)
        {
            var dict = new Dictionary<string, string>();
            dict.Add("userName", getTokenDto.Email);
            dict.Add("password", getTokenDto.Password);
            dict.Add("grant_type", "password");
            var req = new HttpRequestMessage(HttpMethod.Post, "Token") { Content = new FormUrlEncodedContent(dict) };
            var res = await _httpClient.SendAsync(req);
            var token = await res.Content.ReadAsAsync<TokenDto>();

            return token;
        }
    }
}
