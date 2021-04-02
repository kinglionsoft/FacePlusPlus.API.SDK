using System.Text.Json.Serialization;

namespace FacePlusPlus.API.SDK.Models
{
    public class DriverLicense 
    {
        [JsonPropertyName("valid_from")]
        public string ValidFrom { get; set; }

        [JsonPropertyName("gender")]
        public string Gender { get; set; }

        [JsonPropertyName("issued_by")]
        public string IssuedBy { get; set; }

        [JsonPropertyName("issue_date")]
        public string IssueDate { get; set; }

        [JsonPropertyName("class")]
        public string Class { get; set; }

        [JsonPropertyName("license_number")]
        public string LicenseNumber { get; set; }

        [JsonPropertyName("valid_for")]
        public string ValidFor { get; set; }

        [JsonPropertyName("birthday")]
        public string Birthday { get; set; }

        [JsonPropertyName("version")]
        public int Version { get; set; }

        [JsonPropertyName("address")]
        public string Address { get; set; }

        [JsonPropertyName("nationality")]
        public string Nationality { get; set; }

        [JsonPropertyName("type")]
        public int Type { get; set; }

        [JsonPropertyName("side")]
        public string Side { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}