using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EverlyHealth.Models
{
    public class TinyUrlResponse
    {
        [JsonPropertyName("data")]
        public DataStructure? Data { get; set; }

        [JsonPropertyName("errors")]
        public List<string>? Errors { get; set; }

        [JsonPropertyName("code")]
        public int Code { get; set; }
    }

    public class DataStructure
    {
        [JsonPropertyName("url")]
        public string? Url { get; set; }

        [JsonPropertyName("tiny_url")]
        public string? TinyUrl { get; set; }

    }
}
