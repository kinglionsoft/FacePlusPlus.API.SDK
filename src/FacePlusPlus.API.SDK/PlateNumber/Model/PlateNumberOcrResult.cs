#pragma warning disable 8618

using System.Text.Json.Serialization;

namespace FacePlusPlus.API.SDK.Models
{
    public class PlateNumberOcrResult : ApiResult
    {
        [JsonPropertyName("results")]
        public Result[] Results { get; set; }

        public class Result
        {
            //[JsonPropertyName("bound")]
            //public object Bound { get; set; }

            [JsonPropertyName("color")]
            public object Color { get; set; }

            [JsonPropertyName("license_plate_number")]
            public object LicensePlateNumber { get; set; }
        }
    }
}