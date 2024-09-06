using ECommerce.Client.WebUI.Areas.Admin.Models.ProductModels;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace ECommerce.Client.WebUI.Custom.CustomHttpClient
{
    public class CustomHttpClientService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private string BaseUrl;

        public CustomHttpClientService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            BaseUrl = _configuration["ApiBaseUrls:BaseUrl"]!;
        }
        private string CreateUrl(RequestParameters param)
        {
            return $"{(param.baseUrl != null ? $"{param.baseUrl}" : $"{BaseUrl}")}{(param.controller != null ? $"/{param.controller}" : "")}{(param.action != null ? $"/{param.action}" : "")}";

        }
        public async Task<T> Get<T>(RequestParameters param, string? id = null)
        {
            string url = "";
            if (param.fullEndpoint != null)
                url = param.fullEndpoint;
            else
                url = $"{CreateUrl(param)}{(id != null ? $"/{id}" : "")}";

            HttpClient client = _httpClientFactory.CreateClient();
            HttpResponseMessage responseMessage = await client.GetAsync(url);
            if (responseMessage.IsSuccessStatusCode)
            {
                string stringData = await responseMessage.Content.ReadAsStringAsync();
                T jsonData = JsonConvert.DeserializeObject<T>(stringData)!;
                return jsonData;
            }
            else
            {
                // Eğer status code başarılı değilse, hata fırlat
                string errorResponse = await responseMessage.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Request failed with status code {responseMessage.StatusCode}: {errorResponse}");
            }

        }
        public async Task<HttpStatusCode> Post<T>(RequestParameters param, T body)
        {
            string url = "";
            if (param.fullEndpoint != null)
                url = param.fullEndpoint;
            else
                url = CreateUrl(param);
            HttpClient client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(body);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync(url, stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return responseMessage.StatusCode;
            }
            else
            {
                // Eğer status code başarılı değilse, hata fırlat
                string errorResponse = await responseMessage.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Request failed with status code {responseMessage.StatusCode}: {errorResponse}");
            }
        }
        public async Task<HttpStatusCode> Put<T>(RequestParameters param, T body)
        {
            string url = "";
            if (param.fullEndpoint != null)
                url = param.fullEndpoint;
            else
                url = CreateUrl(param);
            HttpClient client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(body);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync(url, stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return responseMessage.StatusCode;
            }
            else
            {
                // Eğer status code başarılı değilse, hata fırlat
                string errorResponse = await responseMessage.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Request failed with status code {responseMessage.StatusCode}: {errorResponse}");
            }
        }
        public async Task<HttpStatusCode> Delete(RequestParameters param, string id)
        {
            string url = "";
            if (param.fullEndpoint != null)
                url = param.fullEndpoint;
            else
                url = $"{CreateUrl(param)}{(id != null ? $"/{id}" : "")}";
         
            HttpClient client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync(url);
            if (responseMessage.IsSuccessStatusCode)
            {
                return responseMessage.StatusCode;
            }
            else
            {
                // Eğer status code başarılı değilse, hata fırlat
                string errorResponse = await responseMessage.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Request failed with status code {responseMessage.StatusCode}: {errorResponse}");
            }
        }
    }
}
