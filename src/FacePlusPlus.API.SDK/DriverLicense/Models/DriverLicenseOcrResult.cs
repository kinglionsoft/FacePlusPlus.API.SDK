#pragma warning disable 8618

using System.Text.Json.Serialization;

namespace FacePlusPlus.API.SDK.Models
{
    public class DriverLicenseOcrResult: ApiResult
    {
        [JsonPropertyName("cards")]
        public DriverLicense[] Cards { get; set; }
    }
}