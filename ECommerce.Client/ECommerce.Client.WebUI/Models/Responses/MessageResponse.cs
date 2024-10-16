using ECommerce.Client.WebUI.Models.Tokens;

namespace ECommerce.Client.WebUI.Models.Responses
{
    public class MessageResponse
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public string? AccessToken { get; set; }
        public DateTime? Expiration { get; set; }
    }
}
