using Microsoft.EntityFrameworkCore;
using Serilog;
using SezerAiWeb.Application.Extensions;
using SezerAiWeb.Infrastructure.Extensions;
using SezerAiWeb.Infrastructure.Hubs;
using SezerAiWeb.Persistence.Extensions;
using SezerAiWeb.Web.Middleware;

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .Build())
    .CreateLogger();

try
{
    Log.Information("Starting SEZER AI Master Control Center");

    var builder = WebApplication.CreateBuilder(args);

    // Use Serilog for logging
    builder.Host.UseSerilog();

    // Add services to the container
    builder.Services.AddControllersWithViews();

    // Add Cookie Authentication with Google OAuth
    builder.Services.AddAuthentication(Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.LoginPath = "/panel/auth/login";
            options.LogoutPath = "/panel/auth/logout";
            options.AccessDeniedPath = "/panel/auth/login";
            options.ExpireTimeSpan = TimeSpan.FromDays(7);
            options.SlidingExpiration = true;
        })
        .AddGoogle(options =>
        {
            options.ClientId = builder.Configuration["Authentication:Google:ClientId"] ?? "";
            options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"] ?? "";
            options.CallbackPath = "/signin-google";
        });

    // Add Health Checks
    builder.Services.AddHealthChecks()
        .AddNpgSql(builder.Configuration.GetConnectionString("DefaultConnection")!, name: "postgresql")
        .AddDbContextCheck<SezerAiWeb.Persistence.Context.ApplicationDbContext>(name: "dbcontext");

    // Add Layer Services (Clean Architecture)
    builder.Services.AddApplicationServices(builder.Configuration);
    builder.Services.AddPersistenceServices(builder.Configuration);
    builder.Services.AddInfrastructureServices(
        hangfireConnectionString: builder.Configuration.GetConnectionString("DefaultConnection")!
    );

    // Legacy services (mevcut yapı)
    var projelerJsonYolu = Path.Combine(builder.Environment.ContentRootPath, "Content", "Projeler", "projeler.json");
    var appDataKlasoru = Path.Combine(builder.Environment.ContentRootPath, "App_Data");
    builder.Services.PersistenceServislerEkle(projelerJsonYolu, appDataKlasoru);

    var blogIcerikKlasoru = Path.Combine(builder.Environment.ContentRootPath, "Content", "Blog");
    builder.Services.InfrastructureServislerEkle(blogIcerikKlasoru);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

// Use Global Exception Handler
app.UseGlobalExceptionHandler();

// TEMPORARY: Authentication disabled for testing
// app.UseAuthentication();
// app.UseAuthorization();

app.MapStaticAssets();

// MasterPanel Area Route
app.MapControllerRoute(
    name: "masterpanel",
    pattern: "panel/{controller=Dashboard}/{action=Index}/{id?}",
    defaults: new { area = "MasterPanel" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

// SignalR Hub endpoint
app.MapHub<DashboardHub>("/hubs/dashboard");

// Health Check endpoint
app.MapHealthChecks("/health");

// Use Infrastructure Services (Hangfire Dashboard)
app.UseInfrastructureServices(enableHangfireDashboard: true);

// Run Migrations and Seed Database
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<SezerAiWeb.Persistence.Context.ApplicationDbContext>();

    // 1. First apply migrations
    await context.Database.MigrateAsync();

    // 2. Then seed data
    await SezerAiWeb.Persistence.SeedData.DatabaseSeeder.SeedAsync(context);
}

// Schedule Background Jobs (after DB is ready)
InfrastructureExtensions.ScheduleBackgroundJobs();

    Log.Information("SEZER AI Master Control Center started successfully");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application start-up failed");
}
finally
{
    Log.CloseAndFlush();
}
