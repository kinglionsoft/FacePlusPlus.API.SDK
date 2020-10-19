using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FacePlusPlus.API.SDK.Models;
using Microsoft.Extensions.Logging;

namespace FacePlusPlus.API.SDK
{
    public partial class FacePlusPlusHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly IOptionBalancer<FacePlusPlusSdkOptions> _optionBalancer;
        private readonly ILogger _logger;
        private readonly IJsonSerializer _jsonSerializer;

        public FacePlusPlusHttpClient(HttpClient httpClient,
            IOptionBalancer<FacePlusPlusSdkOptions> optionBalancer,
            ILogger<FacePlusPlusHttpClient> logger,
            IJsonSerializer jsonSerializer)
        {
            _httpClient = httpClient;
            _optionBalancer = optionBalancer;
            _logger = logger;
            _jsonSerializer = jsonSerializer;
        }

        #region Helpers

        protected FacePlusPlusSdkOptions GetOptions() => _optionBalancer.Next();

        protected virtual async Task<T> PostAsync<T>(string url, MultipartFormDataContent content, CancellationToken cancellation) where T : ApiResult
        {
            var options = GetOptions();
            _httpClient.Timeout = TimeSpan.FromMilliseconds(options.Timeout);
            content.Add(new StringContent(options.ApiKey), "\"api_key\"");
            content.Add(new StringContent(options.ApiSecret), "\"api_secret\"");
            using (content)
            {
                using var response = await _httpClient.PostAsync(url, content, cancellation);
                var responseData = await response.Content.ReadAsByteArrayAsync();
                var apiResult = _jsonSerializer.Deserialize<T>(new ReadOnlySpan<byte>(responseData));
                if (apiResult.Success)
                {
                    return apiResult;
                }

                throw new FacePlusPlusException($"url: {url}, error_msg: {apiResult.ErrorMessage}");
            }
        }

        #endregion
    }
}