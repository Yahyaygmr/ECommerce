using System.Net.Http.Headers;

namespace ECommerce.Client.WebUI.Custom.CustomHttpClient
{
    public class RequestParameters
    {
        public string? controller { get; set; }
        public string? action { get; set; }
        public string? baseUrl { get; set; }
        public string? fullEndpoint { get; set; }
        public HttpHeaders? headers { get; set; }
    }
}
