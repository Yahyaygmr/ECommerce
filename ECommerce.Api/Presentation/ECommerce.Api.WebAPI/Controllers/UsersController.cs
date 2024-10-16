
using ECommerce.Api.Application.Features.AppUsers.Commands.CreateUser;
using ECommerce.Api.Application.Features.AppUsers.Commands.LoginUser;
using ECommerce.Api.WebAPI.ResponseModel;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ECommerce.Api.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserCommandRequest request)
        {
            CreateUserCommandResponse response = await _mediator.Send(request);
            if (response.Succeeded.Equals(false))
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginUserCommandRequest request)
        {
            LoginUserCommandResponse response = await _mediator.Send(request);

            ResponseWrapper<string> wp = new()
            {
                StatusCode = HttpStatusCode.OK,
                AccessToken = response.Token.AccessToken,
                Expiration = response.Token.Expiration,
            };

            return Ok(wp);
        }
    }
}
