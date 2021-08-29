using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MudTracker.Shared;
using MudTracker.Server.Services;

namespace MudTracker.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LocationController : ControllerBase
    {
        private IApiClientService _apiClientService { get; set; }

        public LocationController(IApiClientService apiClientService)
        {
            _apiClientService = apiClientService;
        }

        [HttpGet]
        public async Task<IPAddress> Get()
        {
            return await _apiClientService.GetUserIPAsync();
        }
    }

}