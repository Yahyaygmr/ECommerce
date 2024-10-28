using ECommerce.Api.Application.Dtos.TokenDtos;

namespace ECommerce.Api.Application.Features.AppUsers.Commands.GoogleLogin
{
    public class GoogleLoginCommandResponse
    {
        public Token Token { get; set; }
        public string? Message { get; set; }
    }
}
