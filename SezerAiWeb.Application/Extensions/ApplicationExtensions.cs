using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SezerAiWeb.Application.Interfaces;
using SezerAiWeb.Application.Mapping;
using SezerAiWeb.Application.Services;
using System.Reflection;

namespace SezerAiWeb.Application.Extensions;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // AutoMapper
        services.AddAutoMapper(typeof(MappingProfile).Assembly);

        // FluentValidation
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        // Application Services
        services.AddScoped<IWebsiteService, WebsiteService>();
        services.AddScoped<IMenuService, MenuService>();
        services.AddScoped<IDashboardService, DashboardService>();
        services.AddScoped<INotificationService, NotificationService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ISeoService, SeoService>();

        return services;
    }
}
