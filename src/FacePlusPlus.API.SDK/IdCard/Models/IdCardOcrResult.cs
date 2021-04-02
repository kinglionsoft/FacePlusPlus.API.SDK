#pragma warning disable 8618

using System.Text.Json.Serialization;

namespace FacePlusPlus.API.SDK.Models
{
    public class IdCardOcrResult : ApiResult
    {
        public Card[] Cards { get; set; }

        public class Card
        {
            /// <summary>
            /// 证件类型。返回1，代表是身份证。
            /// </summary>
            public int Type { get; set; }

            public string Address { get; set; }

            public string Birthday { get; set; }

            public string Gender { get; set; }

            [JsonPropertyName("id_card_number")]
            public string IdCardNumber { get; set; }

            public string Name { get; set; }

            /// <summary>
            /// 民族（汉字）
            /// </summary>
            public string Race { get; set; }

            /// <summary>
            /// front: 人像面, back: 国徽面
            /// </summary>
            public string Side { get; set; }
            
            [JsonPropertyName("issued_by")]
            public string IssuedBy { get; set; }
            
            [JsonPropertyName("valid_date")]
            public string ValidDate { get; set; }
        }
    }
}