using System;
using FacePlusPlus.API.SDK;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;

namespace SdkTest
{
    public class TestBase
    {
        protected readonly ITestOutputHelper Output;
        protected readonly IJsonSerializer JsonSerializer;
        protected readonly FacePlusPlusHttpClient FacePlusPlusHttpClient;

        public TestBase(ITestOutputHelper output)
        {
            Output = output;
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("config.json")
                .Build();

            var sp = new ServiceCollection()
                .AddFacePlusPlusSdk(configuration.GetSection("FPP").Bind)
                .BuildServiceProvider();

            FacePlusPlusHttpClient = sp.GetRequiredService<FacePlusPlusHttpClient>();
            JsonSerializer = sp.GetRequiredService<IJsonSerializer>();
        }

        protected void WriteJson(object data) => Output.WriteLine(JsonSerializer.Serialize(data));
    }
}
