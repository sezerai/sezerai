using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SezerAiWeb.Application.Interfaces;
using SezerAiWeb.Persistence.Context;
using SezerAiWeb.Persistence.Repositories;
using SezerAiWeb.Application.Repositories;
using SezerAiWeb.Persistence.Servisler;

namespace SezerAiWeb.Persistence.Extensions;

public static class ServisKayitExtensions
{
    public static IServiceCollection PersistenceServislerEkle(this IServiceCollection services, string projelerJsonYolu, string appDataKlasoru)
    {
        services.Configure<DosyaAyarlari>(opt =>
        {
            opt.ProjelerJsonYolu = projelerJsonYolu;
            opt.AppDataKlasoru = appDataKlasoru;
        });

        services.AddScoped<IProjeServisi, ProjeServisi>();
        services.AddScoped<IIletisimServisi, IletisimServisi>();
        services.AddScoped<IDemoTalebiServisi, DemoTalebiServisi>();

        return services;
    }

    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        // PostgreSQL DbContext Configuration
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(connectionString, npgsqlOptions =>
            {
                npgsqlOptions.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                npgsqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 3,
                    maxRetryDelay: TimeSpan.FromSeconds(10),
                    errorCodesToAdd: null);
            });

            // Enable sensitive data logging in development
            if (configuration.GetValue<bool>("DetailedErrors"))
            {
                options.EnableSensitiveDataLogging();
                options.EnableDetailedErrors();
            }
        });

        // Register Repository Pattern
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IWebsiteRepository, WebsiteRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IUserRoleRepository, UserRoleRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
