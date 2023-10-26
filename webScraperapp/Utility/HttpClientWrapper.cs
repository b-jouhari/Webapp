using Newtonsoft.Json;
using System.Text;

namespace webapi.Utility
{

    public interface IHttpClientWrapper
    {
        public Task<string> GetStringAsync(string url);
    }

    public class HttpClientWrapper : IHttpClientWrapper
    {
        private readonly HttpClient _httpClient;

        public HttpClientWrapper()
        {
            _httpClient = new HttpClient();
        }

        public async Task<string> GetStringAsync(string url)
        {
            return await _httpClient.GetStringAsync(url);
        }

    }
}