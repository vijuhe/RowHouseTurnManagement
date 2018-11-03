using System;
using System.Net.Http;
using System.Threading.Tasks;
using UI.Models;

namespace UI.Services
{
    public class RegistrationService : IRegistrationService
    {
        public async Task<Guid> AddApartment(Apartment apartment)
        {
            using (var httpClient = CreateHttpClient())
            {
                using (HttpResponseMessage response = await httpClient.PostAsJsonAsync("apartments", apartment))
                {
                    response.EnsureSuccessStatusCode();
                    return await response.Content.ReadAsAsync<Guid>();
                }
            }
        }

        private static HttpClient CreateHttpClient()
        {
            var client = new HttpClient { BaseAddress = App.BackendUrl };
            client.DefaultRequestHeaders.Add("ApiKey", App.BackendApiKey.ToString());
            return client;
        }
    }
}
