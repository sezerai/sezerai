using FluentValidation;
using SezerAiWeb.Application.DTOs;

namespace SezerAiWeb.Application.Validators;

public class MenuUpdateDtoValidator : AbstractValidator<MenuUpdateDto>
{
    public MenuUpdateDtoValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Menü ID boş olamaz");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Menü başlığı boş olamaz")
            .MaximumLength(100).WithMessage("Menü başlığı en fazla 100 karakter olabilir");

        RuleFor(x => x.Url)
            .NotEmpty().WithMessage("URL boş olamaz")
            .MaximumLength(500).WithMessage("URL en fazla 500 karakter olabilir");

        RuleFor(x => x.Order)
            .GreaterThanOrEqualTo(0).WithMessage("Sıra numarası 0 veya daha büyük olmalıdır");

        RuleFor(x => x.Icon)
            .MaximumLength(100).WithMessage("İkon adı en fazla 100 karakter olabilir");

        RuleFor(x => x.CssClass)
            .MaximumLength(200).WithMessage("CSS sınıfı en fazla 200 karakter olabilir");

        RuleFor(x => x.AllowedRoles)
            .MaximumLength(500).WithMessage("İzin verilen roller en fazla 500 karakter olabilir");
    }
}
