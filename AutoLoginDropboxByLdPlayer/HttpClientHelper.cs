using System.Net.Http;
using System.Threading.Tasks;
using System;

namespace AutoLoginDropboxByLdPlayer
{
    internal class HttpClientHelper
    {
        private readonly HttpClient _httpClient;
        public HttpClientHelper()
        {
            _httpClient = new HttpClient();
        }

        public async Task<HttpResponseMessage> GetAsync(string relativeUrl)
        {
            try
            {
                // Thực hiện yêu cầu GET
                var response = await _httpClient.GetAsync(relativeUrl);
                return response;
            }
            catch (HttpRequestException httpEx)
            {
                // Xử lý các lỗi liên quan đến HTTP request và ghi log
                Console.WriteLine($"HTTP request error: {httpEx.Message}");
                throw; // Ném lại ngoại lệ để hàm gọi có thể xử lý
            }
            catch (Exception ex)
            {
                // Xử lý các lỗi khác và ghi log
                Console.WriteLine($"Unexpected error: {ex.Message}");
                throw; // Ném lại ngoại lệ để hàm gọi có thể xử lý
            }
        }

        public async Task<HttpResponseMessage> PostAsync(string relativeUrl, HttpContent content)
        {
            try
            {
                // await AddAuthorizationHeader(client);
                var response = await _httpClient.PostAsync(relativeUrl, content);
                return response;
            }
            catch (HttpRequestException httpEx)
            {
                // Xử lý các lỗi liên quan đến HTTP request và ghi log
                Console.WriteLine($"HTTP request error: {httpEx.Message}");
                throw; // Ném lại ngoại lệ để hàm gọi có thể xử lý
            }
            catch (Exception ex)
            {
                // Xử lý các lỗi khác và ghi log
                Console.WriteLine($"Unexpected error: {ex.Message}");
                throw; // Ném lại ngoại lệ để hàm gọi có thể xử lý
            }
        }

        public async Task<HttpResponseMessage> PutAsync(string relativeUrl, HttpContent content)
        {
            try
            {
                var response = await _httpClient.PutAsync(relativeUrl, content);
                return response;
            }
            catch (HttpRequestException httpEx)
            {
                // Xử lý các lỗi liên quan đến HTTP request và ghi log
                Console.WriteLine($"HTTP request error: {httpEx.Message}");
                throw; // Ném lại ngoại lệ để hàm gọi có thể xử lý
            }
            catch (Exception ex)
            {
                // Xử lý các lỗi khác và ghi log
                Console.WriteLine($"Unexpected error: {ex.Message}");
                throw; // Ném lại ngoại lệ để hàm gọi có thể xử lý
            }
        }
    }
}
