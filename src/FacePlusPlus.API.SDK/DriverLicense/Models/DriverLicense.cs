using System;
using System.Text.Json.Serialization;
#pragma warning disable 8618

namespace FacePlusPlus.API.SDK.Models
{
    public class DriverLicense 
    {
        [JsonPropertyName("gender")]
        public string Gender { get; set; }

        [JsonPropertyName("issued_by")]
        public string IssuedBy { get; set; }

        [JsonPropertyName("issue_date")]
        public DateTime IssueDate { get; set; }

        [JsonPropertyName("class")]
        public string Class { get; set; }

        [JsonPropertyName("license_number")]
        public string LicenseNumber { get; set; }

        [JsonPropertyName("birthday")]
        public DateTime Birthday { get; set; }

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

        /// <summary>
        /// 有效日期，格式为YYYY-MM-DD
        /// </summary>
        [JsonPropertyName("valid_from")]
        public DateTime ValidFrom { get; set; }

        /// <summary>
        /// 有效年限，例如 6年
        /// </summary>
        [JsonPropertyName("valid_for")]
        public string ValidFor { get; set; }

        /// <summary>
        /// 有效期限格式为：YYYY-MM-DD至YYYY-MM-DD
        /// </summary>
        [JsonPropertyName("valid_date")]
        public string ValidDate { get; set; }

        public DateTime ValidTo { get; set; }

        public void FixValidDate()
        {
            // 根据驾驶证版本不同，一种情况会返回valid_from和valid_for两个字段，另一种情况只返回valid_date字段。
            if (!string.IsNullOrEmpty(ValidDate))
            {
                var dates = ValidDate.Split("至", StringSplitOptions.RemoveEmptyEntries);
                ValidFrom = DateTime.Parse(dates[0]);
                ValidTo = DateTime.Parse(dates[1]);
            }
            else
            {
                ValidTo = ValidFrom.AddYears(int.Parse(ValidFor.Replace("年", string.Empty)));
            }
        }
    }
}