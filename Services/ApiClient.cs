// Services/ApiClient.cs
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using PunchKioskMobile.Platforms.Shared;

namespace PunchKioskMobile.Services
{
    public class ApiClient
    {
        private readonly HttpClient _http;

        public ApiClient(HttpClient http)
        {
            _http = http;
            _http.BaseAddress = new Uri(BackendConfig.BaseUrl);
            _http.Timeout = TimeSpan.FromSeconds(30);
        }

        public async Task<T> GetAsync<T>(string url)
        {
            var resp = await _http.GetAsync(url);
            resp.EnsureSuccessStatusCode();
            return await resp.Content.ReadFromJsonAsync<T>();
        }

        public async Task<TResponse> PostAsync<TRequest, TResponse>(string url, TRequest payload)
        {
            var resp = await _http.PostAsJsonAsync(url, payload);
            resp.EnsureSuccessStatusCode();
            return await resp.Content.ReadFromJsonAsync<TResponse>();
        }

        public async Task<HttpResponseMessage> PostAsync<TRequest>(string url, TRequest payload)
        {
            return await _http.PostAsJsonAsync(url, payload);
        }
    }
}
