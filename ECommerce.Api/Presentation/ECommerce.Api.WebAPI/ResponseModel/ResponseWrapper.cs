using ECommerce.Api.Application.Dtos.TokenDtos;
using System.Net;

namespace ECommerce.Api.WebAPI.ResponseModel
{
    public class ResponseWrapper<T>
    {
        public T? Data { get; set; } // API'den dönen veri
        public string? Message { get; set; } // API'den dönen mesaj
        public HttpStatusCode StatusCode { get; set; } // HTTP statü kodu
        public bool IsSuccess => ((int)StatusCode >= 200 && (int)StatusCode <= 299); // Başarı kontrolü
        public string? AccessToken { get; set; }
        public DateTime? Expiration { get; set; }
    }
}
