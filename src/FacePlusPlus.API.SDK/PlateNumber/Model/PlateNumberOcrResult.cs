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

            private string _colorName;

            public string ColorName
            {
                get => _colorName ??= MapColor(ColorNumber);
                set => _colorName = value;
            }

            [JsonPropertyName("license_plate_number")]
            public string LicensePlateNumber { get; set; }

            private static string MapColor(int color) => color switch
            {
                0 => "blue",
                1 => "yellow",
                2 => "black",
                3 => "white",
                4 => "green",
                5 => "green",
                6 => "green",
                7 => "missing",
                8 => "invalid",
                _ => "invalid"
            };
        }
    }
}