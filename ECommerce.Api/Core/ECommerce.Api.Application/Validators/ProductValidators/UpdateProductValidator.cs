using ECommerce.Api.Application.ViewModels.ProductViewModels;
using FluentValidation;


namespace ECommerce.Api.Application.Validators.ProductValidators
{
    public class UpdateProductValidator : AbstractValidator<UpdateProductViewModel>
    {
        public UpdateProductValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Lütfen Id Alanını boş geçmeyiniz");

            RuleFor(p => p.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("Lütfen ürün adını boş geçmeyiniz")
                .MinimumLength(5)
                .MaximumLength(150)
                .WithMessage("Lütfen ürün adını 5 ile 150 karakter arasında giriniz.");

            RuleFor(p => p.Stock)
                .NotEmpty()
                .NotNull()
                .WithMessage("Lütfen stok bilgisini boş geçmeyiniz")
                .Must(s => s >= 0)
                .WithMessage("Stok bilgisi negatif olamaz");

            RuleFor(p => p.Price)
               .NotEmpty()
               .NotNull()
               .WithMessage("Lütfen fiyat bilgisini boş geçmeyiniz")
               .Must(p => p >= 0)
               .WithMessage("Fiyat bilgisi negatif olamaz");
        }
    }
}
