using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace KmnlkCommon.Shareds
{
    public class WebClientManagement
    {
        private static HttpClient httpClient;
        private static JsonSerializerSettings MicrosoftDateFormatSettings
        {
            get
            {
                return new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                };
            }
        }
        public static async Task<T> GetAsync<T>(Uri requestUrl)
        {
            addHeaders();
            if (httpClient == null)
            {
                httpClient = new HttpClient();
            }
            var response = await httpClient.GetAsync(requestUrl, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(data);
        }
        public static HttpContent CreateHttpContent<T>(T content)
        {
            var json = JsonConvert.SerializeObject(content, MicrosoftDateFormatSettings);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }
        public static async Task<T> PostAsync<T>(Uri requestUrl, T content)
        {
            addHeaders();
            var response = await httpClient.PostAsync(requestUrl.ToString(), CreateHttpContent<T>(content));
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(data);
        }
        private static void addHeaders()
        {
            //httpClient.DefaultRequestHeaders.Remove("userIP");
            //httpClient.DefaultRequestHeaders.Add("userIP", "192.168.1.1");
        }
        public static Uri CreateRequestUri(string url, string queryString = "")
        {
            var endpoint = new Uri(url);
            var uriBuilder = new UriBuilder(endpoint);
            uriBuilder.Query = queryString;
            return uriBuilder.Uri;
        }

    }
}
