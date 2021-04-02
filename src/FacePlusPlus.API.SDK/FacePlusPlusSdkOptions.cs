#pragma warning disable 8618
namespace FacePlusPlus.API.SDK
{
    public sealed class FacePlusPlusSdkOptions
    {
        public string ApiKey { get; set; }

        public string ApiSecret { get; set; }

        /// <summary>
        /// Request timeout, in ms.
        /// </summary>
        public int Timeout { get; set; } = 10_000;

        /// <summary>
        /// Retry durations in ms, <see cref="Microsoft.Extensions.Http.Polly"/>
        /// </summary>
        public int[] RetryDurations { get; set; } = {500, 1000, 2000};

        public bool DisableServerSslValidation { get; set; }

        public void CloneFrom(FacePlusPlusSdkOptions options)
        {
            ApiKey = options.ApiKey;
            ApiSecret = options.ApiSecret;
            Timeout = options.Timeout;
            RetryDurations = options.RetryDurations;
            DisableServerSslValidation = options.DisableServerSslValidation;
        }
    }
}