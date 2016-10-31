using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MvvmCrossTemplate.Core.Extensions;
using MvvmCrossTemplate.Core.Interfaces.Services;
using MvvmCrossTemplate.Core.Utils;
using MvvmCrossTemplate.Core.Utils.Enums;
using MvvmCrossTemplate.Core.Utils.Onsight.Core.Utils;

namespace MvvmCrossTemplate.Core.Services
{
    public class ApiService : IApiService
    {
        private readonly IConnectivityService _connectivityService;
        public const int NetworkRetryDelayInMillis = 10000;
        public const int NetworkRetries = 3;

        public ApiService(IConnectivityService connectivityService)
        {
            _connectivityService = connectivityService;
        }

        public async Task<Result<string>> GetJsonAsync(string uri, CancellationToken token)
        {
            var getResult = await GetHttpResponseMessageAsync(uri, token);
            if (getResult.IsFailure)
                return Result.Fail<string>(this, getResult);

            string responseJson;
            try
            {
                responseJson = await getResult.Value.Content.ReadAsStringAsync();
            }
            catch (Exception e)
            {
                return Result.Fail<string>(this, ErrorType.ProcessingDownloadedData, e).AddData("uri", uri);
            }

            return Result.Ok(responseJson);
        }

        public async Task<Result<string>> PostJsonAsync(string uri, string content, CancellationToken token, Dictionary<string, string> headers = null)
        {
            var connected = await _connectivityService.IsConnected(NetworkRetries, NetworkRetryDelayInMillis);
            if (!connected)
                return Result.Fail<string>(this, ErrorType.ConnectionCheckFailed);

            var httpClient = GetHttpClient(5, headers);
            var httpContent = new StringContent(content, new UTF8Encoding(), "application/json");

            var responseResult = await Try.ActionNTimesWithWait(() => httpClient.PostAsync(uri, httpContent, token), NetworkRetries, NetworkRetryDelayInMillis,
                ErrorType.DownloadingData);

            if (responseResult.IsFailure)
                return Result.Fail<string>(this, responseResult).AddData(nameof(uri), uri).AddData(nameof(content), content);

            HttpResponseMessage response = responseResult.Value;

            string responseJson;
            try
            {
                responseJson = await response.Content.ReadAsStringAsync();
            }
            catch (Exception e)
            {
                return Result.Fail<string>(this, ErrorType.ProcessingDownloadedData, e).AddData("uri", uri).AddData("content", content);
            }

            return Result.Ok(responseJson);
        }

        public async Task<Result<string>> PutJsonAsync(string uri, string content, CancellationToken token, Dictionary<string, string> headers = null)
        {
            var connected = await _connectivityService.IsConnected(NetworkRetries, NetworkRetryDelayInMillis);
            if (!connected)
                return Result.Fail<string>(this, ErrorType.ConnectionCheckFailed);

            var httpClient = GetHttpClient(5, headers);
            var httpContent = new StringContent(content, new UTF8Encoding(), "application/json");

            var responseResult = await Try.ActionNTimesWithWait(() => httpClient.PutAsync(uri, httpContent, token), NetworkRetries, NetworkRetryDelayInMillis,
                    ErrorType.DownloadingData);
            if (responseResult.IsFailure)
                return Result.Fail<string>(this, responseResult).AddData(nameof(uri), uri).AddData(nameof(content), content);

            HttpResponseMessage response = responseResult.Value;

            string responseJson;
            try
            {
                responseJson = await response.Content.ReadAsStringAsync();
            }
            catch (Exception e)
            {
                return Result.Fail<string>(this, ErrorType.ProcessingDownloadedData, e).AddData("uri", uri).AddData("content", content);
            }

            return Result.Ok(responseJson);
        }

        public async Task<Result<HttpResponseMessage>> GetHttpResponseMessageAsync(string uri, CancellationToken token, Dictionary<string, string> headers = null)
        {
            var connected = await _connectivityService.IsConnected(NetworkRetries, NetworkRetryDelayInMillis);
            if (!connected)
                return Result.Fail<HttpResponseMessage>(this, ErrorType.ConnectionCheckFailed);

            var httpClient = GetHttpClient(5, headers);

            var responseResult = await Try.ActionNTimesWithWait(() => httpClient.GetAsync(uri, token), NetworkRetries, NetworkRetryDelayInMillis,
                ErrorType.DownloadingData);
            if (responseResult.IsFailure)
                return Result.Fail<HttpResponseMessage>(this, responseResult).AddData(nameof(uri), uri).AddData(nameof(headers), headers?.ToArrayString());

            HttpResponseMessage response = responseResult.Value;

            if (!response.IsSuccessStatusCode)
            {
                return Result.Fail<HttpResponseMessage>(this, ErrorType.ServerError)
                    .AddData("HttpError", response.ReasonPhrase)
                    .AddData("HttpErrorCode", response.StatusCode.ToString());
            }

            return Result.Ok(response);
        }

        #region PRIVATES

        private static HttpClient GetHttpClient(int timeOutInMinutes, Dictionary<string, string> headers)
        {
            var httpClient = new HttpClient { Timeout = new TimeSpan(0, 0, timeOutInMinutes, 0) };
            if (headers != null)
            {
                foreach (KeyValuePair<string, string> header in headers)
                {
                    httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            }
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return httpClient;
        }

        #endregion

    }
}