using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EverlyHealth.Models
{
    public class TinyUrlRequest
    {
        public TinyUrlRequest(string url)
        {
            Url = url;
        }

        [JsonPropertyName("url")]
        public string? Url { get; set; }
    }
}
