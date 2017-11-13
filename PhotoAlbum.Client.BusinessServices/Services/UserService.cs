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

        public async Task<HttpStatusCode> GetTokenAsync(GetTokenDto getTokenDto)
        {
            HttpContent content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("username", getTokenDto.Email),
                    new KeyValuePair<string, string>("password", getTokenDto.Password)
                });
            HttpResponseMessage result = _httpClient.PostAsync("Token", content).Result;
            string resultContent = result.Content.ReadAsStringAsync().Result;

            var token = JsonConvert.DeserializeObject<JToken>(resultContent);

            return HttpStatusCode.Accepted;
        }
    }
}
