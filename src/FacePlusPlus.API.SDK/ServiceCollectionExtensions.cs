using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using FacePlusPlus.API.SDK.Internal;
using FacePlusPlus.API.SDK.Internal.Load;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Polly;

namespace FacePlusPlus.API.SDK
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddFacePlusPlusSdk(this IServiceCollection services, 
            Action<List<FacePlusPlusSdkOptions>> configure)
        {
            Debug.Assert(configure != null);

            var options = new List<FacePlusPlusSdkOptions>();

            configure(options);
            
            services.AddOptionBalancer<FacePlusPlusSdkOptions>(opt =>
            {
                opt.AddRange(options);
            });

            var httpClientBuilder = services.AddHttpClient<FacePlusPlusHttpClient>();

            var firstOption = options[0];
            if (firstOption.DisableServerSslValidation)
            {
                httpClientBuilder.ConfigurePrimaryHttpMessageHandler(x => new HttpClientHandler
                {
                    ClientCertificateOptions = ClientCertificateOption.Manual,
                    ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => true
                });
            }

            if (firstOption.RetryDurations?.Length > 0)
            {
                httpClientBuilder.AddTransientHttpErrorPolicy(p =>
                    p.WaitAndRetryAsync(firstOption.RetryDurations.Select(x => TimeSpan.FromMilliseconds(x))));
            }

            services.AddSingleton<IJsonSerializer, JsonSerializer>();

            return services;
        }

        private static IServiceCollection AddOptionBalancer<T>(this IServiceCollection services, Action<List<T>> config)
        {
            services.Configure<List<T>>(config);

            services.AddSingleton<IOptionBalancer<T>>(sp =>
            {
                var optionAccessor = sp.GetService<IOptions<List<T>>>();
                return new OptionBalancer<T>(optionAccessor.Value);
            });

            return services;
        }
    }
}
