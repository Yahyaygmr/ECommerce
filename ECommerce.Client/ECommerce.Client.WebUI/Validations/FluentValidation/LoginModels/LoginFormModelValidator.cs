using ECommerce.Client.WebUI.Models.LoginModels;
using FluentValidation;

namespace ECommerce.Client.WebUI.Validations.FluentValidation.LoginModels
{
    public class LoginFormModelValidator : AbstractValidator<LoginFormModel>
    {
        public LoginFormModelValidator()
        {
            RuleFor(x => x.UserName)
          .NotEmpty().WithMessage("Kullanıcı adı boş olamaz")
          .MinimumLength(6).WithMessage("Lütfen Geçerli Bir Kullanıcı Adı veya Mail Girin");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Şifre boş olamaz")
                .MinimumLength(6).WithMessage("Lütfen Geçerli Bir Şifre Girin");
        }
    }
}
