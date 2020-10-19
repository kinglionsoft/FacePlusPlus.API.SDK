using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace SdkTest
{
    public class OcrTest : TestBase
    {
        public OcrTest(ITestOutputHelper output) : base(output)
        {
        }

        #region IdCard

        [Theory]
        [InlineData(@"data\idcard-1.jpg")]
        public async Task IdCardByFile(string file)
        {
            var result = await FacePlusPlusHttpClient.IdCardByFileAsync(file, default);
            Assert.True(result.Success);
            WriteJson(result);
        }

        [Theory]
        [InlineData(@"data\idcard-1.jpg")]
        public async Task IdCardByStream(string file)
        {
            await using var fs = File.OpenRead(file);
            var result = await FacePlusPlusHttpClient.IdCardByStreamAsync(fs, default);
            Assert.True(result.Success);

            WriteJson(result);
        }

        [Theory]
        [InlineData(@"data\idcard-1.jpg")]
        public async Task IdCardByData(string file)
        {
            var bytes = await File.ReadAllBytesAsync(file);
            var result = await FacePlusPlusHttpClient.IdCardByBytesAsync(bytes, default);
            Assert.True(result.Success);

            WriteJson(result);
        }

        [Theory]
        [InlineData(@"data\idcard-1.jpg")]
        public async Task IdCardByBase64(string file)
        {
            var bytes = await File.ReadAllBytesAsync(file);
            var base64 = Convert.ToBase64String(bytes);
            var result = await FacePlusPlusHttpClient.IdCardByBase64Async(base64, default);
            Assert.True(result.Success);

            WriteJson(result);
        }

        [Theory]
        [InlineData("https://ai.bdstatic.com/file/3C8C5B451BB4445697730217EC8648E3")]
        public async Task IdCardByUrl(string url)
        {
            var result = await FacePlusPlusHttpClient.IdCardByUrlAsync(url, default);
            Assert.True(result.Success);

            WriteJson(result);
        }

        #endregion
    }
}