using ECommerce.Api.Application.Dtos.TokenDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Api.Application.Features.AppUsers.Commands.LoginUser
{
    public class LoginUserCommandResponse
    {
        public Token? Token { get; set; }
        public string? Message { get; set; }
    }
}
