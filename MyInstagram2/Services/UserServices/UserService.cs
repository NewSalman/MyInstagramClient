using MyInstagram2.Models;
using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyInstagram2.Services.UserServices
{
    public class UserService : BaseHttpClient, IUserService
    {

        public UserService()
        {
        }

        public async Task<AutenticatedUser> Login(TokenRequest model)
        {
            AutenticatedUser user = new AutenticatedUser();

            Uri uri = new Uri(string.Format(RestUri.uri, "/api/mobile/user/login"));

            string json = JsonConvert.SerializeObject(model);

            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage response = await _client.PostAsync(uri, content);

                string ResponseContent = await response.Content.ReadAsStringAsync();

                user = JsonConvert.DeserializeObject<AutenticatedUser>(ResponseContent);
               
                if(user.IsAuthenticated == false)
                {
                    return user;
                }
                await App.Database.saveToken(user);
                return user;
            } catch (HttpRequestException e)
            {
                user.IsAuthenticated = false;
                user.Message = $"Connection time out, {e.Message}";
                return user;
            } 
        }
    }
}
