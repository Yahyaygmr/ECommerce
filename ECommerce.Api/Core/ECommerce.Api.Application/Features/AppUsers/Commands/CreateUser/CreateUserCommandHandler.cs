using ECommerce.Api.Application.Exceptions.AppUserExceptions;
using ECommerce.Api.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ECommerce.Api.Application.Features.AppUsers.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {
        private readonly UserManager<AppUser> _userManager;

        public CreateUserCommandHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            IdentityResult result = await _userManager.CreateAsync(new()
            {
                Id=Guid.NewGuid().ToString(),
                Name = request.Name,
                Surname = request.Surname,
                Email = request.Mail,
                UserName = request.UserName,
            }, request.Password);

            var res = result.Errors;

            CreateUserCommandResponse response = new() { Succeeded=result.Succeeded};
            if (result.Succeeded)
            {
                response.Message = "Kullanıcı Başarıyla Oluşturuldu.";
            }
            else
            {
                foreach (var error in result.Errors)
                    response.Message += $"{error.Code} - {error.Description}<br>";
                //response.Message = "Kullanıcı Oluşturulurken Beklenmedik Bir Hata Oluştu";
            }
            return response;
        }
    }
}
