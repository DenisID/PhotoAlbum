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

        public async Task<TokenDto> GetTokenAsync(GetTokenDto getTokenDto)
        {
            //TokenDto token = null;

            var dict = new Dictionary<string, string>();
            dict.Add("userName", getTokenDto.Email);
            dict.Add("password", getTokenDto.Password);
            dict.Add("grant_type", "password");
            var client = new HttpClient();
            var req = new HttpRequestMessage(HttpMethod.Post, "http://localhost:52670/Token") { Content = new FormUrlEncodedContent(dict) };
            var res = await client.SendAsync(req);
            var result = await res.Content.ReadAsAsync<TokenDto>();
            //var str = await res.Content.ReadAsStringAsync();
            //var result = JsonConvert.DeserializeObject<TokenDto>(str);

            return result;

            //HttpContent content = new FormUrlEncodedContent(new[]
            //    {
            //        new KeyValuePair<string, string>("grant_type", "password"),
            //        new KeyValuePair<string, string>("username", getTokenDto.Email),
            //        new KeyValuePair<string, string>("password", getTokenDto.Password)
            //    });
            //object result = _httpClient.PostAsync("Token", content).Result;
            //string resultContent = result.Content.ReadAsStringAsync().Result;

            //var token = JsonConvert.DeserializeObject<JToken>(resultContent);

            throw new NotImplementedException();

            //return HttpStatusCode.Accepted;
        }
    }
}
