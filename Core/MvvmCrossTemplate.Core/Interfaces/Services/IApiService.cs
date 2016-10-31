using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using MvvmCrossTemplate.Core.Utils;

namespace MvvmCrossTemplate.Core.Interfaces.Services
{
    public interface IApiService
    {
        Task<Result<string>> GetJsonAsync(string uri, CancellationToken token);
        Task<Result<string>> PostJsonAsync(string uri, string jsonContent, CancellationToken token, Dictionary<string, string> headers = null);
        Task<Result<string>> PutJsonAsync(string uri, string content, CancellationToken token, Dictionary<string, string> headers = null);
        Task<Result<HttpResponseMessage>> GetHttpResponseMessageAsync(string uri, CancellationToken token, Dictionary<string, string> headers = null);
    }
}