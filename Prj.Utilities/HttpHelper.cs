using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Prj.Utilities
{
    public static class HttpHelper
    {
        public static async Task CallApiAsGet(string apiUrl)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                await client.GetAsync(apiUrl);
            }
        }

        public static async Task CallApiAsPost(string apiUrl, object value)
        {
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Ssl3;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                await client.PostAsJsonAsync(apiUrl, value);
            }
        }

        public static async Task<T> CallApiAsPost<T>(string apiUrl, object value)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                var response = await client.PostAsJsonAsync(apiUrl, value);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<T>();
                }
                return default(T);
            }
        }

        public static async Task<T> CallApiAsFormEncoded<T>(string apiUrl, IEnumerable<KeyValuePair<string, string>> value)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                var content = new FormUrlEncodedContent(value);
                var response = await client.PostAsync(apiUrl, content);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<T>();
                }
                return default(T);
            }
        }
    }
}
