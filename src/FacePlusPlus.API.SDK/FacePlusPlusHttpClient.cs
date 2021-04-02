using System;
using System.Net.Http;
using System.Text.Json;
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
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public FacePlusPlusHttpClient(HttpClient httpClient,
            IOptionBalancer<FacePlusPlusSdkOptions> optionBalancer,
            ILogger<FacePlusPlusHttpClient> logger,
            JsonSerializerOptions? jsonSerializerOptions = null)
        {
            _httpClient = httpClient;
            _optionBalancer = optionBalancer;
            _logger = logger;
            _jsonSerializerOptions = jsonSerializerOptions ?? new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
                IgnoreNullValues = true
            };
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
#if DEBUG
                var responseData = await response.Content.ReadAsStringAsync(cancellation);
#else
                var responseData = await response.Content.ReadAsByteArrayAsync(cancellation);
#endif
                var apiResult = Deserialize<T>(responseData);
                if (apiResult!.Success)
                {
                    return apiResult;
                }

                throw new FacePlusPlusException($"url: {url}, error_msg: {apiResult.ErrorMessage}");
            }
        }

        protected virtual string Serialize(object data) => JsonSerializer.Serialize(data, _jsonSerializerOptions);

        protected virtual T? Deserialize<T>(string json) => JsonSerializer.Deserialize<T>(json, _jsonSerializerOptions);

        protected virtual T? Deserialize<T>(byte[] json) => JsonSerializer.Deserialize<T>(json, _jsonSerializerOptions);

        #endregion
    }
}