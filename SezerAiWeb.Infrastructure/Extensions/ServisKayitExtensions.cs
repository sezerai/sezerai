using Microsoft.Extensions.DependencyInjection;
using SezerAiWeb.Application.Interfaces;
using SezerAiWeb.Infrastructure.Blog;

namespace SezerAiWeb.Infrastructure.Extensions;

public static class ServisKayitExtensions
{
    public static IServiceCollection InfrastructureServislerEkle(this IServiceCollection services, string blogIcerikKlasoru)
    {
        services.Configure<BlogAyarlari>(opt => opt.IcerikKlasoru = blogIcerikKlasoru);
        services.AddScoped<IBlogServisi, BlogServisi>();

        return services;
    }
}
