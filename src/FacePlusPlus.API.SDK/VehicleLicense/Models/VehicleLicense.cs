#pragma warning disable 8618

using System;
using System.Text.Json.Serialization;

namespace FacePlusPlus.API.SDK.Models
{
    public class VehicleLicense
    {
        [JsonPropertyName("issue_date")]
        public DateTime IssueDate { get; set; }

        [JsonPropertyName("vehicle_type")]
        public string VehicleType { get; set; }

        [JsonPropertyName("issued_by")]
        public string IssuedBy { get; set; }

        [JsonPropertyName("vin")]
        public string Vin { get; set; }

        [JsonPropertyName("plate_no")]
        public string PlateNo { get; set; }

        [JsonPropertyName("side")]
        public string Side { get; set; }

        [JsonPropertyName("use_character")]
        public string UseCharacter { get; set; }

        [JsonPropertyName("address")]
        public string Address { get; set; }

        [JsonPropertyName("owner")]
        public string Owner { get; set; }

        [JsonPropertyName("model")]
        public string Model { get; set; }

        [JsonPropertyName("register_date")]
        public string RegisterDate { get; set; }

        [JsonPropertyName("type")]
        public int Type { get; set; }

        [JsonPropertyName("engine_no")]
        public string EngineNo { get; set; }
    }
}