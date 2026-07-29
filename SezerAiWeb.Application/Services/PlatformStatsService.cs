using Npgsql;
using SezerAiWeb.Application.DTOs;
using SezerAiWeb.Application.Interfaces;

namespace SezerAiWeb.Application.Services;

public class PlatformStatsService : IPlatformStatsService
{
    private readonly string _host;
    private readonly string _username;
    private readonly string _password;

    public PlatformStatsService(string host, string username, string password)
    {
        _host = host;
        _username = username;
        _password = password;
    }

    public async Task<PlatformUserCountDto> GetPlatformUserCountsAsync()
    {
        var result = new PlatformUserCountDto
        {
            LastUpdated = DateTime.UtcNow
        };

        // Geliyoo - HzYusa database, AspNetUsers table (Identity)
        result.GeliyooUserCount = await GetUserCountAsync("HzYusa", "AspNetUsers", "AGeylani", "HisaR3466!");

        // AiHospital - Tibbiye database, Users table
        result.AiHospitalUserCount = await GetUserCountAsync("Tibbiye", "Users", "AGeylani", "HisaR3466!");

        // AiBazaar - bakkal database, Kullanicilar table
        result.AiBazaarUserCount = await GetUserCountAsync("bakkal", "Kullanicilar", "bakkalamca", "HisaR3466!");

        // Perde Imalat - pencere database, Kullanici table (Migration yapılmamış)
        result.PerdeImalatUserCount = await GetUserCountAsync("pencere", "Kullanici", "perdeci", "HisaR3466!");

        return result;
    }

    private async Task<int> GetUserCountAsync(string database, string tableName, string username, string password)
    {
        try
        {
            var connectionString = $"Host={_host};Database={database};Username={username};Password={password}";

            await using var connection = new NpgsqlConnection(connectionString);
            await connection.OpenAsync();

            var query = $"SELECT COUNT(*) FROM \"{tableName}\"";
            await using var command = new NpgsqlCommand(query, connection);

            var result = await command.ExecuteScalarAsync();
            return Convert.ToInt32(result);
        }
        catch (Exception)
        {
            // Hata durumunda 0 döndür
            return 0;
        }
    }
}
