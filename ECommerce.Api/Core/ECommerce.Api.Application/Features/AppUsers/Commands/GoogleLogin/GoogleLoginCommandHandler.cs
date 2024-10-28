using ECommerce.Api.Application.Abstraction.Token;
using ECommerce.Api.Application.Dtos.TokenDtos;
using ECommerce.Api.Domain.Entities.Identity;
using Google.Apis.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace ECommerce.Api.Application.Features.AppUsers.Commands.GoogleLogin
{
    public class GoogleLoginCommandHandler : IRequestHandler<GoogleLoginCommandRequest, GoogleLoginCommandResponse>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenHandler _tokenHandler;

        public GoogleLoginCommandHandler(UserManager<AppUser> userManager, ITokenHandler tokenHandler)
        {
            _userManager = userManager;
            _tokenHandler = tokenHandler;
        }

        public async Task<GoogleLoginCommandResponse> Handle(GoogleLoginCommandRequest request, CancellationToken cancellationToken)
        {
            ValidationSettings settings = new ValidationSettings()
            {
                Audience = new List<string>() { "779574934829-mhlqpdljv4pnk05t3o4eslhvjiug82t3.apps.googleusercontent.com" }
            };
            var payload = await ValidateAsync(request.IdToken, settings);

            var info = new UserLoginInfo(request.Provider, payload.Subject, request.Provider);

            AppUser? user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
            bool result = user != null;

            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(payload.Email);
                if (user == null)
                {
                    user = new()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Email = payload.Email,
                        UserName = payload.Email,
                        Name = payload.Name,
                        Surname = payload.FamilyName,
                    };
                   var identityResult = await _userManager.CreateAsync(user);
                    result = identityResult.Succeeded;

                }
            }
            if (result)
            {
                await _userManager.AddLoginAsync(user, info);
            }
            else
            {
                throw new Exception("Invalid external authentication.");
            }

            Token token = _tokenHandler.CreateAccessToken(5);
            return new()
            {
                Token = token,
            };
        }
    }
}
