using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SezerAiWeb.Domain.Entities;
using SezerAiWeb.Persistence.Context;

namespace SezerAiWeb.Persistence.SeedData;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        // Ensure database created
        await context.Database.EnsureCreatedAsync();

        // Seed Roles
        if (!await context.Roles.AnyAsync())
        {
            await SeedRolesAsync(context);
        }

        // Seed Admin User
        if (!await context.Users.AnyAsync())
        {
            await SeedAdminUserAsync(context);
        }

        // Seed Websites
        if (!await context.Websites.AnyAsync())
        {
            await SeedWebsitesAsync(context);
        }

        await context.SaveChangesAsync();
    }

    private static async Task SeedRolesAsync(ApplicationDbContext context)
    {
        var roles = new List<Role>
        {
            new Role
            {
                Id = Guid.NewGuid(),
                Name = "SuperAdmin",
                Description = "Full system access with all permissions",
                CreatedAt = DateTime.UtcNow
            },
            new Role
            {
                Id = Guid.NewGuid(),
                Name = "Admin",
                Description = "Administrative access to manage websites and users",
                CreatedAt = DateTime.UtcNow
            },
            new Role
            {
                Id = Guid.NewGuid(),
                Name = "Viewer",
                Description = "Read-only access to dashboard and metrics",
                CreatedAt = DateTime.UtcNow
            }
        };

        await context.Roles.AddRangeAsync(roles);
        await context.SaveChangesAsync();
    }

    private static async Task SeedAdminUserAsync(ApplicationDbContext context)
    {
        // Password: Admin123!
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword("Admin123!");

        var superAdminRole = await context.Roles.FirstAsync(r => r.Name == "SuperAdmin");

        var adminUser = new User
        {
            Id = Guid.NewGuid(),
            Email = "admin@sezerai.tr",
            PasswordHash = hashedPassword,
            FirstName = "SEZER",
            LastName = "AI Admin",
            IsEmailVerified = true,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        await context.Users.AddAsync(adminUser);
        await context.SaveChangesAsync();

        // Assign SuperAdmin role
        var userRole = new UserRole
        {
            Id = Guid.NewGuid(),
            UserId = adminUser.Id,
            RoleId = superAdminRole.Id,
            CreatedAt = DateTime.UtcNow
        };

        await context.UserRoles.AddAsync(userRole);
        await context.SaveChangesAsync();

        Console.WriteLine("✅ Admin user created successfully!");
        Console.WriteLine($"   Email: {adminUser.Email}");
        Console.WriteLine("   Password: Admin123!");
    }

    private static async Task SeedWebsitesAsync(ApplicationDbContext context)
    {
        var adminUser = await context.Users.FirstAsync(u => u.Email == "admin@sezerai.tr");

        var websites = new List<Website>
        {
            new Website
            {
                Id = Guid.NewGuid(),
                Name = "TR-AI",
                Domain = "www.tr-ai.com",
                Description = "AI Teknoloji Platformu - Yapay zeka çözümleri ve hizmetleri",
                LogoUrl = "/images/logos/tr-ai.png",
                IsActive = true,
                ContactEmail = "info@tr-ai.com",
                GoogleAnalyticsId = "G-XXXXXXXXXX",
                MetaTitle = "TR-AI - Yapay Zeka Platformu",
                MetaDescription = "Türkiye'nin önde gelen AI teknoloji platformu",
                Language = "tr-TR",
                Currency = "TRY",
                TimeZone = "Europe/Istanbul",
                OwnerId = adminUser.Id,
                CreatedAt = DateTime.UtcNow
            },
            new Website
            {
                Id = Guid.NewGuid(),
                Name = "AI-Hospital",
                Domain = "www.ai-hospital.com",
                Description = "Sağlık Yönetim Sistemi - Hastane ve klinik otomasyon çözümleri",
                LogoUrl = "/images/logos/ai-hospital.png",
                IsActive = true,
                ContactEmail = "info@ai-hospital.com",
                GoogleAnalyticsId = "G-YYYYYYYYYY",
                MetaTitle = "AI Hospital - Sağlık Yönetim Sistemi",
                MetaDescription = "Yapay zeka destekli hastane yönetim platformu",
                Language = "tr-TR",
                Currency = "TRY",
                TimeZone = "Europe/Istanbul",
                OwnerId = adminUser.Id,
                CreatedAt = DateTime.UtcNow
            },
            new Website
            {
                Id = Guid.NewGuid(),
                Name = "AiBazaar",
                Domain = "www.aibazaar.tr",
                Description = "E-ticaret Platformu - Online alışveriş merkezi",
                LogoUrl = "/images/logos/aibazaar.png",
                IsActive = true,
                ContactEmail = "destek@aibazaar.tr",
                GoogleAnalyticsId = "G-ZZZZZZZZZZ",
                MetaTitle = "AiBazaar - Online Alışveriş",
                MetaDescription = "Türkiye'nin yeni nesil e-ticaret platformu",
                Language = "tr-TR",
                Currency = "TRY",
                TimeZone = "Europe/Istanbul",
                OwnerId = adminUser.Id,
                CreatedAt = DateTime.UtcNow
            },
            new Website
            {
                Id = Guid.NewGuid(),
                Name = "Geliyoo",
                Domain = "www.geliyoo.com",
                Description = "Hizmet Platformu - Profesyonel hizmet sağlayıcıları ağı",
                LogoUrl = "/images/logos/geliyoo.png",
                IsActive = true,
                ContactEmail = "info@geliyoo.com",
                GoogleAnalyticsId = "G-AAAAAAAAAA",
                MetaTitle = "Geliyoo - Hizmet Platformu",
                MetaDescription = "Uzman hizmet sağlayıcılarla buluşma noktası",
                Language = "tr-TR",
                Currency = "TRY",
                TimeZone = "Europe/Istanbul",
                OwnerId = adminUser.Id,
                CreatedAt = DateTime.UtcNow
            },
            new Website
            {
                Id = Guid.NewGuid(),
                Name = "Perde-İmalat",
                Domain = "www.perdeimalat.com",
                Description = "E-ticaret Platformu - Perde ve ev tekstili ürünleri",
                LogoUrl = "/images/logos/perde-imalat.png",
                IsActive = true,
                ContactEmail = "siparis@perdeimalat.com",
                GoogleAnalyticsId = "G-BBBBBBBBBB",
                MetaTitle = "Perde İmalat - Ev Tekstili",
                MetaDescription = "Kaliteli perde ve ev tekstili ürünleri",
                Language = "tr-TR",
                Currency = "TRY",
                TimeZone = "Europe/Istanbul",
                OwnerId = adminUser.Id,
                CreatedAt = DateTime.UtcNow
            }
        };

        await context.Websites.AddRangeAsync(websites);
        await context.SaveChangesAsync();

        Console.WriteLine("✅ 5 Website seeded successfully!");
        foreach (var website in websites)
        {
            Console.WriteLine($"   - {website.Name} ({website.Domain})");
        }
    }
}
