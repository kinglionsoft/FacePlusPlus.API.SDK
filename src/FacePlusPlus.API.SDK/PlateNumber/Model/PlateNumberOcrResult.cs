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
            [JsonPropertyName("color")]
            public int ColorNumber { get; set; }

            public string ColorName { get; set; }

            [JsonPropertyName("license_plate_number")]
            public string LicensePlateNumber { get; set; }
        }
    }
}