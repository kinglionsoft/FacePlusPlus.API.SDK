#pragma warning disable 8618

using System.Text.Json.Serialization;

namespace FacePlusPlus.API.SDK.Models
{
    public class VehicleLicenseOcrResult : ApiResult
    {
        [JsonPropertyName("cards")]
        public VehicleLicense[] Cards { get; set; }
    }
}