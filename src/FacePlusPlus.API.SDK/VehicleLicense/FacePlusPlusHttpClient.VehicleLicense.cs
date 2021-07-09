using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FacePlusPlus.API.SDK.Internal;
using FacePlusPlus.API.SDK.Models;

namespace FacePlusPlus.API.SDK
{
    public partial class FacePlusPlusHttpClient
    {
        /// <summary>
        /// refer https://console.faceplusplus.com.cn/documents/5671706
        /// </summary>
        /// <param name="url">absolute url of the image</param>
        /// <returns></returns>
        public Task<VehicleLicenseOcrResult> VehicleLicenseByUrlAsync(string url,CancellationToken cancellation = default)
        {
            if (string.IsNullOrEmpty(url)) throw new ArgumentNullException(nameof(url));

            return VehicleLicenseAsync(m =>
            {
                m.Add(new StringContent(url), "\"image_url\"");
            }, cancellation);
        }

        /// <summary>
        /// refer https://console.faceplusplus.com.cn/documents/5671706
        /// </summary>
        /// <param name="file">absolute path of the image</param>
        /// <returns></returns>
        public async Task<VehicleLicenseOcrResult> VehicleLicenseByFileAsync(string file, CancellationToken cancellation = default)
        {
            if (string.IsNullOrEmpty(file)) throw new ArgumentNullException(nameof(file));

            Utils.EnsureImageFormat(file);
            await using var fs = File.OpenRead(file);
            return await VehicleLicenseByStreamAsync(fs, cancellation);
        }

        /// <summary>
        /// refer https://console.faceplusplus.com.cn/documents/5671706
        /// </summary>
        /// <param name="fileStream">stream of the image</param>
        /// <returns></returns>
        public async Task<VehicleLicenseOcrResult> VehicleLicenseByStreamAsync(Stream fileStream, CancellationToken cancellation = default)
        {
            if (fileStream == null) throw new ArgumentNullException(nameof(fileStream));

            return await VehicleLicenseAsync(m =>
            {
                m.Add(new StreamContent(fileStream), "\"image_file\"", "test.png");
            }, cancellation);
        }

        /// <summary>
        /// refer https://console.faceplusplus.com.cn/documents/5671706
        /// </summary>
        /// <param name="data">bytes of the image</param>
        /// <returns></returns>
        public async Task<VehicleLicenseOcrResult> VehicleLicenseByBytesAsync(byte[] data, CancellationToken cancellation = default)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            await using var memory = new MemoryStream(data);
            return await VehicleLicenseByStreamAsync(memory, cancellation);
        }

        /// <summary>
        /// refer https://console.faceplusplus.com.cn/documents/5671706
        /// </summary>
        /// <param name="base64">Base64 of the image</param>
        /// <returns></returns>
        public async Task<VehicleLicenseOcrResult> VehicleLicenseByBase64Async(string base64, CancellationToken cancellation = default)
        {
            if (string.IsNullOrEmpty(base64)) throw new ArgumentNullException(nameof(base64));

            return await VehicleLicenseAsync(m =>
            {
                m.Add(new StringContent(base64), "\"image_base64\"");
            }, cancellation);
        }

        protected virtual async Task<VehicleLicenseOcrResult> VehicleLicenseAsync(Action<MultipartFormDataContent> config, CancellationToken cancellation = default)
        {
            var multi = new MultipartFormDataContent();
            config(multi);
            var result = await PostAsync<VehicleLicenseOcrResult>("https://api-cn.faceplusplus.com/cardpp/v1/ocrvehiclelicense", multi, cancellation);
            return result;
        }
    }
}