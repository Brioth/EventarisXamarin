using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Eventaris.Domain;
using Newtonsoft.Json;

namespace EventarisXDal
{
    public class UserService : IUserService
    {
        private const string baseUrl = "https://eventaris.azurewebsites.net/api/users";
        //private const string baseUrl = "http://localhost:3164/api/users";


        public User GetById(int id)
        {
            using (var client = new HttpClient())
            {
                Uri url = new Uri(String.Concat(baseUrl,"/",id));
                var response = "";
                Task task = Task.Run(async ()=>
                    {
                        response = await client.GetStringAsync(url);
                    });

                task.Wait();

                var requestedUser = JsonConvert.DeserializeObject<User>(response);

                return requestedUser;
            }
        }

        public User GetByName(string firstName, string LastName)
        {
            using (var client = new HttpClient())
            {
                Uri uri = new Uri(baseUrl);
                var response = "";
                Task task = Task.Run(async () => { response = await client.GetStringAsync(uri); });
                task.Wait();
                var allUsers = JsonConvert.DeserializeObject<List<User>>(response);

                return allUsers.Find(u => u.FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase) && u.LastName.Equals(LastName, StringComparison.OrdinalIgnoreCase));
            }
        }

        public async Task<bool> New(User user)  
        {
            using (var client = new HttpClient())
            {
                Uri uri = new Uri(baseUrl);
                var request = new HttpRequestMessage(HttpMethod.Post, uri);
                var hc = new StringContent(JsonConvert.SerializeObject(user));
                hc.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                request.Content = hc;
                var response = await client.SendAsync(request);

                return response.IsSuccessStatusCode;
            }

        }

        public async Task<bool> Update(User user)
        {   
            using (var client = new HttpClient())
            {
                
                    Uri uri = new Uri(String.Concat(baseUrl,"/",user.Id));
                var request = new HttpRequestMessage(HttpMethod.Put, uri);
                var hc = new StringContent(JsonConvert.SerializeObject(user));
                hc.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                request.Content = hc;
                var response = await client.SendAsync(request);

                return response.IsSuccessStatusCode;

            }
        }
    }
}
