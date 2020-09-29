using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using FacePlusPlus.API.SDK.Internal;
using Microsoft.Extensions.DependencyInjection;
using Polly;

namespace FacePlusPlus.API.SDK
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddFacePlusPlusSdk(this IServiceCollection services, Action<FacePlusPlusSdkOptions> configure)
        {
            Debug.Assert(configure != null);

            var options = new FacePlusPlusSdkOptions();

            configure(options);

            services.Configure<FacePlusPlusSdkOptions>(opt => opt.CloneFrom(options));

            var httpClientBuilder = services.AddHttpClient<FacePlusPlusHttpClient>();

            if (options.DisableServerSslValidation)
            {
                httpClientBuilder.ConfigurePrimaryHttpMessageHandler(x => new HttpClientHandler
                {
                    ClientCertificateOptions = ClientCertificateOption.Manual,
                    ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => true
                });
            }

            if (options.RetryDurations?.Length > 0)
            {
                httpClientBuilder.AddTransientHttpErrorPolicy(p =>
                    p.WaitAndRetryAsync(options.RetryDurations.Select(x => TimeSpan.FromMilliseconds(x))));
            }

            services.AddSingleton<IJsonSerializer, JsonSerializer>();

            return services;
        }
    }
}
