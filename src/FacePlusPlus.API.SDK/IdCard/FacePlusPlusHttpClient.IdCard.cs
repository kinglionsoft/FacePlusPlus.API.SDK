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
        /// refer https://console.faceplusplus.com.cn/documents/5671702
        /// </summary>
        /// <param name="url">absolute url of the image</param>
        /// <returns></returns>
        public Task<IdCardOcrResult> IdCardByUrlAsync(string url, CancellationToken cancellation)
        {
            if (string.IsNullOrEmpty(url)) throw new ArgumentNullException(nameof(url));

            return IdCardAsync(m => m.Add(new StringContent(url), "\"image_url\""), cancellation);
        }

        /// <summary>
        /// refer https://console.faceplusplus.com.cn/documents/5671702
        /// </summary>
        /// <param name="file">absolute path of the image</param>
        /// <returns></returns>
        public async Task<IdCardOcrResult> IdCardByFileAsync(string file, CancellationToken cancellation)
        {
            if (string.IsNullOrEmpty(file)) throw new ArgumentNullException(nameof(file));

            Utils.EnsureImageFormat(file);
            await using var fs = File.OpenRead(file);
            return await IdCardByStreamAsync(fs, cancellation);
        }

        /// <summary>
        /// refer https://console.faceplusplus.com.cn/documents/5671702
        /// </summary>
        /// <param name="fileStream">stream of the image</param>
        /// <returns></returns>
        public async Task<IdCardOcrResult> IdCardByStreamAsync(Stream fileStream, CancellationToken cancellation)
        {
            if (fileStream == null) throw new ArgumentNullException(nameof(fileStream));

            return await IdCardAsync(m => m.Add(new StreamContent(fileStream), "\"image_file\"", "test.png"), cancellation);
        }

        /// <summary>
        /// refer https://console.faceplusplus.com.cn/documents/5671702
        /// </summary>
        /// <param name="data">bytes of the image</param>
        /// <returns></returns>
        public async Task<IdCardOcrResult> IdCardByBytesAsync(byte[] data, CancellationToken cancellation)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            await using var memory = new MemoryStream(data);
            return await IdCardByStreamAsync(memory, cancellation);
        }

        /// <summary>
        /// refer https://console.faceplusplus.com.cn/documents/5671702
        /// </summary>
        /// <param name="base64">Base64 of the image</param>
        /// <returns></returns>
        public async Task<IdCardOcrResult> IdCardByBase64Async(string base64, CancellationToken cancellation)
        {
            if (string.IsNullOrEmpty(base64)) throw new ArgumentNullException(nameof(base64));

            return await IdCardAsync(m => m.Add(new StringContent(base64), "\"image_base64\""), cancellation);
        }

        protected virtual async Task<IdCardOcrResult> IdCardAsync(Action<MultipartFormDataContent> config, CancellationToken cancellation)
        {
            var multi = new MultipartFormDataContent();
            config(multi);
            var result = await PostAsync<IdCardOcrResult>("https://api-cn.faceplusplus.com/cardpp/v1/ocridcard", multi, cancellation);
            return result;
        }
    }
}