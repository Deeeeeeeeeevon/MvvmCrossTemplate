using System;
using System.Threading.Tasks;
using MvvmCrossTemplate.Core.Interfaces.Services;
using MvvmCrossTemplate.Core.Utils;
using MvvmCrossTemplate.Core.Utils.Enums;
using Plugin.Connectivity;

namespace MvvmCrossTemplate.Core.Services
{
    public class ConnectivityService : IConnectivityService
    {

        public async Task<ConnectionState> GetConnectionStateAsync()
        {
            bool isConnected = await IsHostReachableAsync(3, 10000);
            return new ConnectionState { IsConnected = isConnected };
        }

        public async Task<bool> IsConnected(int retries, int delayInMillis)
        {
            try
            {
                for (int i = 0; i < retries; i++)
                {
                    var connected = CrossConnectivity.Current.IsConnected;
                    if (connected)
                        return true;

                    if (i < (retries - 1)) await Task.Delay(delayInMillis); //Don't await the last call
                }
            }
            catch (Exception e)
            {
                //TODO do something with error
                Error error = Error.Create(this, ErrorType.ConnectionCheckFailed, e);
                return false;
            }
            return false;
        }

        public async Task<bool> IsHostReachableAsync(int retries = 1, int delayInMillis = 0)
        {
            try
            {
                for (int i = 0; i < retries; i++)
                {
                    var hostReachable = await CrossConnectivity.Current.IsRemoteReachable("http://www.google.com");
                    if (hostReachable)
                        return true;

                    if (i < (retries - 1)) await Task.Delay(delayInMillis); //Don't await the last call
                }
            }
            catch (Exception e)
            {
                //TODO do something with error
                Error error = Error.Create(this, ErrorType.HostNotReachable, e);
                return false;
            }
            return false;
        }
    }
}