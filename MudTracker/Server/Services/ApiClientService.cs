using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using MudTracker.Shared;

namespace MudTracker.Server.Services
{
    public class ApiClientService: IApiClientService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        
        public ApiClientService(IHttpClientFactory httpClientfactory)
        {
            _httpClientFactory = httpClientfactory;
        }
        
        public async Task<IPAddress> GetUserIPAsync()
        {
            var client =  _httpClientFactory.CreateClient("IP");
            return await client.GetFromJsonAsync<IPAddress>("/");
        }
    }
}