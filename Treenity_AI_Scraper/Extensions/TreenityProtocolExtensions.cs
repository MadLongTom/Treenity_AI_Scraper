using System.Net.Http.Json;
using Polly;

namespace Treenity_AI_Scraper.Extensions
{
    public static class TreenityProtocolExtensions
    {
        public class TreenityProtocolBaseModel
        {
            public int? status { get; set; }
            public int? code { get; set; }
            public string message { get; set; }
            public long? date { get; set; }
        }

        public static async Task<T?> TreenityGetFromJsonAsync<T>(this HttpClient client, string url) where T : TreenityProtocolBaseModel
        {
            var retryPolicy = Policy.Handle<Exception>().RetryAsync(5);
            return await retryPolicy.ExecuteAsync(async () =>
            {
                var res = await client.GetAsync(url);
                if (res.IsSuccessStatusCode)
                {
                    var baseJson = await res.Content.ReadFromJsonAsync<T>() ?? throw new Exception("JsonSerializer Error");
                    if (baseJson.code.HasValue)
                    {
                        return baseJson.code == 0
                            ? baseJson
                            : throw new Exception("Error while fetching data from Treenity Resource");
                    }
                    else if (baseJson.status.HasValue)
                    {
                        return baseJson.status == 200
                            ? baseJson
                            : throw new Exception("Error while fetching data from Treenity Resource");
                    }
                    else throw new Exception("Error while fetching data from Treenity Resource");
                }
                else
                {
                    throw new Exception("Error while fetching data from Treenity Gateway");
                }
            });
        }
        public static async Task<string> TreenityGetStringAsync(this HttpClient client, string url)
        {
            var retryPolicy = Policy.Handle<Exception>().RetryAsync(5);
            return await retryPolicy.ExecuteAsync(async () =>
            {
                var res = await client.GetAsync(url);
                if (res.IsSuccessStatusCode)
                {
                    var baseJson = await res.Content.ReadFromJsonAsync<TreenityProtocolBaseModel>() ?? throw new Exception("JsonSerializer Error");
                    if (baseJson.code.HasValue)
                    {
                        return baseJson.code == 0
                            ? await res.Content.ReadAsStringAsync()
                            : throw new Exception("Error while fetching data from Treenity Resource");
                    }
                    else if (baseJson.status.HasValue)
                    {
                        return baseJson.status == 200
                            ? await res.Content.ReadAsStringAsync()
                            : throw new Exception("Error while fetching data from Treenity Resource");
                    }
                    else throw new Exception("Error while fetching data from Treenity Resource");
                }
                else
                {
                    throw new Exception("Error while fetching data from Treenity Gateway");
                }
            });
        }

        public static async Task<T?> TreenityPostAsJsonAsync<T>(this HttpClient client, string url, HttpContent ctx) where T : TreenityProtocolBaseModel
        {
            var retryPolicy = Policy.Handle<Exception>().RetryAsync(5);
            return await retryPolicy.ExecuteAsync(async () =>
            {
                var res = await client.PostAsync(url, ctx);
                if (res.IsSuccessStatusCode)
                {
                    var baseJson = await res.Content.ReadFromJsonAsync<T>() ?? throw new Exception("JsonSerializer Error");
                    if (baseJson.code.HasValue)
                    {
                        return baseJson.code == 0
                            ? baseJson
                            : throw new Exception("Error while fetching data from Treenity Resource");
                    }
                    else if (baseJson.status.HasValue)
                    {
                        return baseJson.status == 200
                            ? baseJson
                            : throw new Exception("Error while fetching data from Treenity Resource");
                    }
                    else throw new Exception("Error while fetching data from Treenity Resource");
                }
                else
                {
                    throw new Exception("Error while fetching data from Treenity Gateway");
                }
            });
        }
        public static async Task<string> TreenityPostStringAsync(this HttpClient client, string url,HttpContent ctx)
        {
            var retryPolicy = Policy.Handle<Exception>().RetryAsync(5);
            return await retryPolicy.ExecuteAsync(async () =>
            {
                var res = await client.PostAsync(url,ctx);
                if (res.IsSuccessStatusCode)
                {
                    var baseJson = await res.Content.ReadFromJsonAsync<TreenityProtocolBaseModel>() ?? throw new Exception("JsonSerializer Error");
                    if (baseJson.code.HasValue)
                    {
                        return baseJson.code == 0
                            ? await res.Content.ReadAsStringAsync()
                            : throw new Exception("Error while fetching data from Treenity Resource");
                    }
                    else if(baseJson.status.HasValue)
                    {
                        return baseJson.status == 200
                            ? await res.Content.ReadAsStringAsync()
                            : throw new Exception("Error while fetching data from Treenity Resource");
                    }
                    else throw new Exception("Error while fetching data from Treenity Resource");
                }
                else
                {
                    throw new Exception("Error while fetching data from Treenity Gateway");
                }
            });
        }
    }
}
