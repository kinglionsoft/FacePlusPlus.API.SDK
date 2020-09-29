using System.Text.Json.Serialization;

namespace FacePlusPlus.API.SDK.Models
{
    public class ApiResult
    {
        [JsonPropertyName("request_id")]
        public string RequestId { get; set; }

        [JsonPropertyName("image_id")]
        public string ImageId { get; set; }

        [JsonPropertyName("time_used")]
        public int TimeUsed { get; set; }

        [JsonPropertyName("error_message")]
        public string ErrorMessage { get; set; }

        [JsonIgnore]
        public bool Success => string.IsNullOrEmpty(ErrorMessage);
    }
}