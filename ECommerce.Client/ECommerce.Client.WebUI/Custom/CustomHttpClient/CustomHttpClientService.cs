using ECommerce.Client.WebUI.Areas.Admin.Models.ProductModels;
using ECommerce.Client.WebUI.Models.Responses;
using ECommerce.Client.WebUI.Models.ReturnVals;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;

namespace ECommerce.Client.WebUI.Custom.CustomHttpClient
{
    public class CustomHttpClientService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;
        private string BaseUrl;

        public CustomHttpClientService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _client = _httpClientFactory.CreateClient();
            _configuration = configuration;
            BaseUrl = _configuration["ApiBaseUrls:BaseUrl"]!;
        }
        private string CreateUrl(RequestParameters param)
        {
            return $"{(param.baseUrl != null ? $"{param.baseUrl}" : $"{BaseUrl}")}{(param.controller != null ? $"/{param.controller}" : "")}{(param.action != null ? $"/{param.action}" : "")}";

        }
        public async Task<ResponseWrapper<T>> Get<T>(RequestParameters param, string? id = null)
        {
            string url = "";
            if (param.fullEndpoint != null)
                url = param.fullEndpoint;
            else
                url = $"{CreateUrl(param)}{(id != null ? $"/{id}" : "")}{(param.querystring != null ? $"?{param.querystring}" : "")}";


            HttpResponseMessage responseMessage = await _client.GetAsync(url);
            var response = new ResponseWrapper<T> { StatusCode = responseMessage.StatusCode };
            if (responseMessage.IsSuccessStatusCode)
            {
                string stringData = await responseMessage.Content.ReadAsStringAsync();
                response.Data = JsonConvert.DeserializeObject<T>(stringData)!;
                response.Message = "Başarılı";
            }
            else
            {
                response.Message = await responseMessage.Content.ReadAsStringAsync();

            }
            return response;

        }
        public async Task<ResponseWrapper<string>> Post<T>(RequestParameters param, T body)
        {
            string url = "";
            if (param.fullEndpoint != null)
                url = param.fullEndpoint;
            else
                url = $"{CreateUrl(param)}{(param.querystring != null ? $"?{param.querystring}" : "")}";
            var jsonData = JsonConvert.SerializeObject(body);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await _client.PostAsync(url, stringContent);

            var response = new ResponseWrapper<string> { StatusCode = responseMessage.StatusCode };

            if (responseMessage.IsSuccessStatusCode)
            {
                response.Message = await responseMessage.Content.ReadAsStringAsync();
            }
            else
            {
                response.Message = await responseMessage.Content.ReadAsStringAsync();
            }

            return response;
        }
        public async Task<ResponseWrapper<string>> PostData<T>(RequestParameters param, T body)
        {
            string url = "";
            if (param.fullEndpoint != null)
                url = param.fullEndpoint;
            else
                url = $"{CreateUrl(param)}{(param.querystring != null ? $"?{param.querystring}" : "")}";

            HttpClient client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(body);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            MultipartFormDataContent dataContent = new();
            dataContent.Add(stringContent, "jsonData");

            if (body is IEnumerable<IFormFile> files)
            {
                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        var streamContent = new StreamContent(file.OpenReadStream());
                        streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);
                        dataContent.Add(streamContent, "files", file.FileName);
                    }
                }
            }
            var responseMessage = await client.PostAsync(url, dataContent);
            var response = new ResponseWrapper<string> { StatusCode = responseMessage.StatusCode };

            if (responseMessage.IsSuccessStatusCode)
            {
                response.Message = await responseMessage.Content.ReadAsStringAsync();
            }
            else
            {
                response.Message = await responseMessage.Content.ReadAsStringAsync();
            }

            return response;
        }
        public async Task<ResponseWrapper<string>> Put<T>(RequestParameters param, T body)
        {
            string url = "";
            if (param.fullEndpoint != null)
                url = param.fullEndpoint;
            else
                url = $"{CreateUrl(param)}{(param.querystring != null ? $"?{param.querystring}" : "")}";

            var jsonData = JsonConvert.SerializeObject(body);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await _client.PutAsync(url, stringContent);
            var response = new ResponseWrapper<string> { StatusCode = responseMessage.StatusCode };

            if (responseMessage.IsSuccessStatusCode)
            {
                response.Message = await responseMessage.Content.ReadAsStringAsync();
            }
            else
            {
                response.Message = await responseMessage.Content.ReadAsStringAsync();
            }

            return response;
        }
        public async Task<ResponseWrapper<string>> Delete(RequestParameters param, string id)
        {
            string url = "";
            if (param.fullEndpoint != null)
                url = param.fullEndpoint;
            else
                url = $"{CreateUrl(param)}{(id != null ? $"/{id}" : "")}{(param.querystring != null ? $"?{param.querystring}" : "")}";

            HttpClient client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync(url);
            var response = new ResponseWrapper<string> { StatusCode = responseMessage.StatusCode };

            if (responseMessage.IsSuccessStatusCode)
            {
                response.Message = "Silme işlemi başarılı.";
            }
            else
            {
                response.Message = await responseMessage.Content.ReadAsStringAsync();
            }

            return response;
        }
    }
}
