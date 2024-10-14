using ECommerce.Client.WebUI.Models.RegisterModels;
using FluentValidation;

namespace ECommerce.Client.WebUI.Validations.FluentValidation.RegisterModel
{
    public class RegisterFormModelValidator : AbstractValidator<RegisterFormModel>
    {
        public RegisterFormModelValidator()
        {
            // Name Validation
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("İsim Alanı Boş Olamaz")
                .Length(2, 50).WithMessage("İsim 2 ile 50 Karakter Aralığında Olmalıdır");

            // Surname Validation
            RuleFor(x => x.Surname)
                .NotEmpty().WithMessage("Soyisim Alanı Boş Olamaz")
                .Length(2, 50).WithMessage("Soyisim 2 ile 50 Karakter Aralığında Olmalıdır");

            // UserName Validation
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Kukkanıcı Adı Alanı Boş Olamaz")
                .Length(3, 20).WithMessage("Kullanıcı Adı 3 ile 20 Karakter Aralığında Olmalıdır");

            // Mail Validation
            RuleFor(x => x.Mail)
                .NotEmpty().WithMessage("Mail Alanı Boş Alamaz")
                .EmailAddress().WithMessage("Geçersiz Format");

            // Password Validation
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Şifre Alanı Boş Alamaz")
                .MinimumLength(6).WithMessage("Şifre En Az 6 Karakter Olmalıdır");

            // ConfirmPassword Validation
            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Şifre Tekrarı Alanı Boş Alamaz")
                .Equal(x => x.Password).WithMessage("Şifreler Eşleşmiyor");

            // Complex Password Validation (optional)
            //RuleFor(x => x.Password)
            //    .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter")
            //    .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter")
            //    .Matches(@"[0-9]").WithMessage("Password must contain at least one number")
            //    .Matches(@"[\W]").WithMessage("Password must contain at least one special character");
        }
    }
}
