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

        #region DriverLicense

        [Theory]
        [InlineData("https://ai.bdstatic.com/file/079EFF6805BB459696EA65933FA1B51C")]
        public async Task OcrDriverLicenseByUrl(string url)
        {
            var result = await FacePlusPlusHttpClient.DriverLicenseByUrlAsync(url, default);

            WriteJson(result);
        }

        #endregion

        #region VehicleLicense

        [Theory]
        [InlineData("https://ai.bdstatic.com/file/545B7A5A048B47E285B883E983EE32A0")]
        public async Task OcrVehicleLicenseByUrl(string url)
        {
            var result = await FacePlusPlusHttpClient.VehicleLicenseByUrlAsync(url, default);

            WriteJson(result);
        }

        #endregion

        #region PlateNumber

        [Theory]
        [InlineData("https://gimg2.baidu.com/image_search/src=http%3A%2F%2Fwww.guohuanjiancai.com%2Fgccxlpm%2Fimages%2F2934349b033b5bb561e1b2e231d3d539b600bcfc.jpg&refer=http%3A%2F%2Fwww.guohuanjiancai.com&app=2002&size=f9999,10000&q=a80&n=0&g=0n&fmt=jpeg?sec=1637831611&t=d498bc38a5062c8fea8892ca51a484c5")]
        public async Task PlateNumberByUrlAsync(string url)
        {
            var result = await FacePlusPlusHttpClient.PlateNumberByUrlAsync(url, default);
            WriteJson(result);
        }

        [Theory]
        [InlineData(@"data\PlateNumber-2.jpg")]
        public async Task PlateNumberByFileAsync(string file)
        {
            var result = await FacePlusPlusHttpClient.PlateNumberByFileAsync(file, default);
            Assert.True(result.Success);
            WriteJson(result);
        }

        [Theory]
        [InlineData(@"data\PlateNumber-2.jpg")]
        public async Task PlateNumberByStreamAsync(string file)
        {
            await using var fs = File.OpenRead(file);
            var result = await FacePlusPlusHttpClient.PlateNumberByStreamAsync(fs, default);
            Assert.True(result.Success);

            WriteJson(result);
        }

        [Theory]
        [InlineData(@"data\PlateNumber-2.jpg")]
        public async Task PlateNumberByBytesAsync(string file)
        {
            var bytes = await File.ReadAllBytesAsync(file);
            var result = await FacePlusPlusHttpClient.PlateNumberByBytesAsync(bytes, default);
            Assert.True(result.Success);

            WriteJson(result);
        }

        [Theory]
        [InlineData(@"data\PlateNumber-4.jpg")]
        public async Task PlateNumberByBase64Async(string file)
        {
            var bytes = await File.ReadAllBytesAsync(file);
            var base64 = Convert.ToBase64String(bytes);
            var result = await FacePlusPlusHttpClient.PlateNumberByBase64Async(base64, default);
            Assert.True(result.Success);

            WriteJson(result);
        }

        #endregion
    }
}