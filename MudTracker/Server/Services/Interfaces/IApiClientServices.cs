using MudTracker.Shared;
using System.Threading.Tasks;

namespace MudTracker.Server.Services
{
    public interface IApiClientService
    {
        Task<IPAddress> GetUserIPAsync();
    }
}