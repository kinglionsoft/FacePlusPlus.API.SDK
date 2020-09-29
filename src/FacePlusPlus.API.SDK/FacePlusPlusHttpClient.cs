using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FacePlusPlus.API.SDK.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FacePlusPlus.API.SDK
{
    public partial class FacePlusPlusHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly FacePlusPlusSdkOptions _options;
        private readonly ILogger _logger;
        private readonly IJsonSerializer _jsonSerializer;

        public FacePlusPlusHttpClient(HttpClient httpClient,
            IOptions<FacePlusPlusSdkOptions> optionAccessor,
            ILogger<FacePlusPlusHttpClient> logger,
            IJsonSerializer jsonSerializer)
        {
            _httpClient = httpClient;
            _logger = logger;
            _jsonSerializer = jsonSerializer;
            _options = optionAccessor.Value;

            _httpClient.Timeout = TimeSpan.FromMilliseconds(_options.Timeout);
        }

        #region Helpers

        protected virtual async Task<T> PostAsync<T>(string url, MultipartFormDataContent content, CancellationToken cancellation) where T : ApiResult
        {
            content.Add(new StringContent(_options.ApiKey), "\"api_key\"");
            content.Add(new StringContent(_options.ApiSecret), "\"api_secret\"");
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