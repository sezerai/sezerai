using FluentValidation;
using SezerAiWeb.Application.DTOs;

namespace SezerAiWeb.Application.Validators;

public class WebsiteUpdateDtoValidator : AbstractValidator<WebsiteUpdateDto>
{
    public WebsiteUpdateDtoValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Website ID zorunludur");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Website adı zorunludur")
            .MaximumLength(200).WithMessage("Website adı maksimum 200 karakter olabilir");

        RuleFor(x => x.Domain)
            .NotEmpty().WithMessage("Domain zorunludur")
            .MaximumLength(255).WithMessage("Domain maksimum 255 karakter olabilir")
            .Matches(@"^[a-zA-Z0-9]([a-zA-Z0-9\-]{0,61}[a-zA-Z0-9])?(\.[a-zA-Z]{2,})+$")
            .WithMessage("Geçerli bir domain formatı giriniz (örn: sezerai.tr)");

        RuleFor(x => x.Description)
            .MaximumLength(1000).WithMessage("Açıklama maksimum 1000 karakter olabilir")
            .When(x => !string.IsNullOrEmpty(x.Description));

        RuleFor(x => x.LogoUrl)
            .MaximumLength(500).WithMessage("Logo URL maksimum 500 karakter olabilir")
            .Must(BeValidUrl).WithMessage("Geçerli bir URL formatı giriniz")
            .When(x => !string.IsNullOrEmpty(x.LogoUrl));

        RuleFor(x => x.FaviconUrl)
            .MaximumLength(500).WithMessage("Favicon URL maksimum 500 karakter olabilir")
            .Must(BeValidUrl).WithMessage("Geçerli bir URL formatı giriniz")
            .When(x => !string.IsNullOrEmpty(x.FaviconUrl));

        RuleFor(x => x.ContactEmail)
            .EmailAddress().WithMessage("Geçerli bir email adresi giriniz")
            .MaximumLength(255).WithMessage("Email maksimum 255 karakter olabilir")
            .When(x => !string.IsNullOrEmpty(x.ContactEmail));

        RuleFor(x => x.ContactPhone)
            .MaximumLength(20).WithMessage("Telefon maksimum 20 karakter olabilir")
            .Matches(@"^[\d\s\+\-\(\)]+$").WithMessage("Geçerli bir telefon formatı giriniz")
            .When(x => !string.IsNullOrEmpty(x.ContactPhone));

        RuleFor(x => x.GoogleAnalyticsId)
            .MaximumLength(50).WithMessage("Google Analytics ID maksimum 50 karakter olabilir")
            .Matches(@"^(UA-|G-)[A-Z0-9\-]+$").WithMessage("Geçerli bir Google Analytics ID formatı giriniz (UA-XXXXXX veya G-XXXXXX)")
            .When(x => !string.IsNullOrEmpty(x.GoogleAnalyticsId));

        RuleFor(x => x.GoogleSearchConsoleId)
            .MaximumLength(100).WithMessage("Google Search Console ID maksimum 100 karakter olabilir")
            .When(x => !string.IsNullOrEmpty(x.GoogleSearchConsoleId));

        RuleFor(x => x.GoogleTagManagerId)
            .MaximumLength(50).WithMessage("Google Tag Manager ID maksimum 50 karakter olabilir")
            .Matches(@"^GTM-[A-Z0-9]+$").WithMessage("Geçerli bir Google Tag Manager ID formatı giriniz (GTM-XXXXXX)")
            .When(x => !string.IsNullOrEmpty(x.GoogleTagManagerId));

        RuleFor(x => x.MetaTitle)
            .MaximumLength(70).WithMessage("Meta title maksimum 70 karakter olmalıdır (SEO optimizasyonu için)")
            .When(x => !string.IsNullOrEmpty(x.MetaTitle));

        RuleFor(x => x.MetaDescription)
            .MaximumLength(160).WithMessage("Meta description maksimum 160 karakter olmalıdır (SEO optimizasyonu için)")
            .When(x => !string.IsNullOrEmpty(x.MetaDescription));

        RuleFor(x => x.MetaKeywords)
            .MaximumLength(255).WithMessage("Meta keywords maksimum 255 karakter olabilir")
            .When(x => !string.IsNullOrEmpty(x.MetaKeywords));

        RuleFor(x => x.Language)
            .MaximumLength(10).WithMessage("Dil kodu maksimum 10 karakter olabilir")
            .Matches(@"^[a-z]{2}(-[A-Z]{2})?$").WithMessage("Geçerli bir dil kodu giriniz (örn: tr-TR, en-US)")
            .When(x => !string.IsNullOrEmpty(x.Language));

        RuleFor(x => x.Currency)
            .MaximumLength(3).WithMessage("Para birimi kodu maksimum 3 karakter olabilir")
            .Matches(@"^[A-Z]{3}$").WithMessage("Geçerli bir para birimi kodu giriniz (örn: TRY, USD, EUR)")
            .When(x => !string.IsNullOrEmpty(x.Currency));

        RuleFor(x => x.TimeZone)
            .MaximumLength(50).WithMessage("TimeZone maksimum 50 karakter olabilir")
            .Must(BeValidTimeZone).WithMessage("Geçerli bir TimeZone giriniz (örn: Europe/Istanbul, America/New_York)")
            .When(x => !string.IsNullOrEmpty(x.TimeZone));
    }

    private bool BeValidUrl(string? url)
    {
        if (string.IsNullOrEmpty(url))
            return true;

        return Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
               && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
    }

    private bool BeValidTimeZone(string? timeZone)
    {
        if (string.IsNullOrEmpty(timeZone))
            return true;

        try
        {
            TimeZoneInfo.FindSystemTimeZoneById(timeZone);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
