using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using SezerAiWeb.Infrastructure.BackgroundJobs;
using SezerAiWeb.Infrastructure.Hubs;
using Hangfire;
using Hangfire.PostgreSql;
using System.Linq.Expressions;

namespace SezerAiWeb.Infrastructure.Extensions;

public static class InfrastructureExtensions
{
    /// <summary>
    /// Infrastructure katmanı servislerini ekler
    /// </summary>
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        string? hangfireConnectionString = null)
    {
        // SignalR ekleme
        services.AddSignalR(options =>
        {
            options.EnableDetailedErrors = true;
            options.MaximumReceiveMessageSize = 102400; // 100 KB
            options.StreamBufferCapacity = 10;
            options.KeepAliveInterval = TimeSpan.FromSeconds(15);
            options.ClientTimeoutInterval = TimeSpan.FromSeconds(30);
        });

        // Background Job servisleri
        services.AddScoped<HealthCheckJob>();
        services.AddScoped<MetricsCollectorJob>();
        services.AddScoped<BackupJob>();

        // Hangfire - PostgreSQL ile
        if (!string.IsNullOrEmpty(hangfireConnectionString))
        {
            services.AddHangfire(config => config
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UsePostgreSqlStorage(options =>
                {
                    options.UseNpgsqlConnection(hangfireConnectionString);
                }));

            // Hangfire server ekleme
            services.AddHangfireServer(options =>
            {
                options.WorkerCount = Environment.ProcessorCount * 5;
                options.ServerName = $"SezerAiWeb-{Environment.MachineName}";
                options.Queues = new[] { "default", "critical", "background" };
            });
        }

        return services;
    }

    /// <summary>
    /// Infrastructure middleware'lerini yapılandırır
    /// </summary>
    public static IApplicationBuilder UseInfrastructureServices(
        this IApplicationBuilder app,
        bool enableHangfireDashboard = true)
    {
        // Hangfire Dashboard
        if (enableHangfireDashboard)
        {
            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[] { new HangfireAuthorizationFilter() },
                DashboardTitle = "SezerAI Web - Background Jobs",
                StatsPollingInterval = 2000,
                DisplayStorageConnectionString = false
            });
        }

        return app;
    }

    /// <summary>
    /// Background job'ları planlar
    /// </summary>
    public static void ScheduleBackgroundJobs()
    {
        // Sağlık kontrolü - Her 5 dakikada bir
        RecurringJob.AddOrUpdate<HealthCheckJob>(
            "health-check",
            job => job.Execute(),
            "*/5 * * * *", // Her 5 dakika
            new RecurringJobOptions
            {
                TimeZone = TimeZoneInfo.Utc
            });

        // Metrik toplama - Her 10 dakikada bir
        RecurringJob.AddOrUpdate<MetricsCollectorJob>(
            "metrics-collector",
            job => job.Execute(),
            "*/10 * * * *", // Her 10 dakika
            new RecurringJobOptions
            {
                TimeZone = TimeZoneInfo.Utc
            });

        // Yedekleme - Her gece saat 02:00'de
        RecurringJob.AddOrUpdate<BackupJob>(
            "database-backup",
            job => job.Execute(),
            "0 2 * * *", // Her gece 02:00
            new RecurringJobOptions
            {
                TimeZone = TimeZoneInfo.Utc
            });
    }

    /// <summary>
    /// Bir kez çalıştırılacak background job ekler
    /// </summary>
    public static string EnqueueJob<T>(Expression<Action<T>> methodCall)
    {
        return BackgroundJob.Enqueue(methodCall);
    }

    /// <summary>
    /// Belirli bir süre sonra çalıştırılacak job ekler
    /// </summary>
    public static string ScheduleJob<T>(Expression<Action<T>> methodCall, TimeSpan delay)
    {
        return BackgroundJob.Schedule(methodCall, delay);
    }
}

/// <summary>
/// Hangfire dashboard için basit yetkilendirme filtresi
/// </summary>
public class HangfireAuthorizationFilter : Hangfire.Dashboard.IDashboardAuthorizationFilter
{
    public bool Authorize(Hangfire.Dashboard.DashboardContext context)
    {
        // TODO: Gerçek yetkilendirme mantığı eklenecek
        // Şimdilik development için herkese izin veriliyor
        // Production'da admin rolü kontrolü eklenmelidir
        return true;
    }
}
