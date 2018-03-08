using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Eventaris.Domain;
using Newtonsoft.Json;

namespace EventarisXDal
{
    public class ParticipationService : IParticipationService
    {
        private const string baseUrl = "https://eventaris.azurewebsites.net/api/events/participation";

        public async Task<bool> RegisterParticipation(Participation participation)
        {
            using (var client = new HttpClient())
            {
                Uri uri = new Uri(baseUrl);
                var request = new HttpRequestMessage(HttpMethod.Post, uri);
                var hc = new StringContent(JsonConvert.SerializeObject(participation));
                hc.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                request.Content = hc;
                var response = await client.SendAsync(request);

                return response.IsSuccessStatusCode;
            }
        }
    }
}
