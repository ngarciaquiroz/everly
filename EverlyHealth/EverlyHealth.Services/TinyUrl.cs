using EverlyHealth.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EverlyHealth.Services
{
    public class TinyUrl : ITinyUrl
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration Configuration;

        public TinyUrl(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            Configuration = configuration;
        }
        public async Task<string?> ShortenUrl(string? url)
        {
            if (String.IsNullOrEmpty(url))
            {
                return null;
            }

            if (url.StartsWith(Configuration["TinyUrl"]))
            {
                return url;
            }
            var requestObject = new TinyUrlRequest(url: url);
            var httpRequestMessage = new HttpRequestMessage(
            HttpMethod.Post,
            "https://api.tinyurl.com/create")
            {
                Headers =
            {
                { HeaderNames.Accept, "application/json" },
                { HeaderNames.Authorization, "Bearer "+ Configuration["TinyUrlToken"] }
            }
            };
            var jsonRequest = JsonSerializer.Serialize(requestObject);
            httpRequestMessage.Content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");


            using (var httpClient = _httpClientFactory.CreateClient())
            using (var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage))
            {

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var contentStream = await httpResponseMessage.Content.ReadAsStringAsync();
                    var tinyUrlResponse = JsonSerializer.Deserialize<TinyUrlResponse>(contentStream);
                    return tinyUrlResponse?.Data?.TinyUrl;

                }
            }
            return null;
        }
    }
}
