using Microsoft.Extensions.Logging;

namespace SezerAiWeb.Infrastructure.BackgroundJobs;

/// <summary>
/// Sistem sağlık kontrolü için periyodik job
/// </summary>
public class HealthCheckJob
{
    private readonly ILogger<HealthCheckJob> _logger;

    public HealthCheckJob(ILogger<HealthCheckJob> logger)
    {
        _logger = logger;
    }

    public async Task Execute()
    {
        _logger.LogInformation("HealthCheckJob başlatıldı: {Time}", DateTime.UtcNow);

        try
        {
            // Veritabanı bağlantı kontrolü
            var dbHealthy = await CheckDatabaseHealth();

            // Bellek kullanımı kontrolü
            var memoryHealthy = CheckMemoryHealth();

            // Disk alanı kontrolü
            var diskHealthy = CheckDiskHealth();

            if (dbHealthy && memoryHealthy && diskHealthy)
            {
                _logger.LogInformation("Sistem sağlığı: OK");
            }
            else
            {
                _logger.LogWarning("Sistem sağlık uyarısı - DB: {Db}, Memory: {Memory}, Disk: {Disk}",
                    dbHealthy, memoryHealthy, diskHealthy);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "HealthCheckJob sırasında hata oluştu");
        }
    }

    private async Task<bool> CheckDatabaseHealth()
    {
        // TODO: Gerçek veritabanı kontrolü implement edilecek
        await Task.Delay(100);
        return true;
    }

    private bool CheckMemoryHealth()
    {
        var currentProcess = System.Diagnostics.Process.GetCurrentProcess();
        var memoryMB = currentProcess.WorkingSet64 / 1024 / 1024;

        _logger.LogInformation("Mevcut bellek kullanımı: {Memory} MB", memoryMB);

        // 1GB üzerindeyse uyarı
        return memoryMB < 1024;
    }

    private bool CheckDiskHealth()
    {
        try
        {
            var driveInfo = new System.IO.DriveInfo(System.IO.Path.GetPathRoot(Environment.CurrentDirectory)!);
            var freeSpaceGB = driveInfo.AvailableFreeSpace / 1024 / 1024 / 1024;

            _logger.LogInformation("Kullanılabilir disk alanı: {Space} GB", freeSpaceGB);

            // 10GB'dan azsa uyarı
            return freeSpaceGB > 10;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Disk kontrolü sırasında hata");
            return false;
        }
    }
}
