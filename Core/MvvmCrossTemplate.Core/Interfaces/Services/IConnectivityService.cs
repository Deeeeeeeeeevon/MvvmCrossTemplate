using System.Threading.Tasks;
using MvvmCrossTemplate.Core.Utils;

namespace MvvmCrossTemplate.Core.Interfaces.Services
{
    public interface IConnectivityService
    {
        Task<ConnectionState> GetConnectionStateAsync();
        Task<bool> IsConnected(int retries, int delayInMillis);
        Task<bool> IsHostReachableAsync(int retries, int delayInMillis);
    }
}