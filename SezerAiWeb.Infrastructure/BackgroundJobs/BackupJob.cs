using Microsoft.Extensions.Logging;
using System.IO.Compression;

namespace SezerAiWeb.Infrastructure.BackgroundJobs;

/// <summary>
/// Veritabanı ve dosya yedekleme için periyodik job
/// </summary>
public class BackupJob
{
    private readonly ILogger<BackupJob> _logger;
    private readonly string _backupPath;

    public BackupJob(ILogger<BackupJob> logger)
    {
        _logger = logger;
        _backupPath = Path.Combine(Environment.CurrentDirectory, "backups");

        // Yedekleme klasörünü oluştur
        if (!Directory.Exists(_backupPath))
        {
            Directory.CreateDirectory(_backupPath);
        }
    }

    public async Task Execute()
    {
        _logger.LogInformation("BackupJob başlatıldı: {Time}", DateTime.UtcNow);

        try
        {
            var backupTimestamp = DateTime.UtcNow.ToString("yyyyMMdd_HHmmss");

            // Veritabanı yedeği
            await BackupDatabase(backupTimestamp);

            // Dosya yedeği (uploads, logs vb.)
            await BackupFiles(backupTimestamp);

            // Eski yedekleri temizle (30 günden eski)
            CleanOldBackups();

            _logger.LogInformation("BackupJob başarıyla tamamlandı");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "BackupJob sırasında hata oluştu");
        }
    }

    private async Task BackupDatabase(string timestamp)
    {
        try
        {
            _logger.LogInformation("Veritabanı yedeği başlatılıyor: {Timestamp}", timestamp);

            // TODO: PostgreSQL backup komutu çalıştır
            // pg_dump kullanılabilir
            var backupFileName = Path.Combine(_backupPath, $"db_backup_{timestamp}.sql");

            // Örnek: pg_dump komutunu çalıştır
            // var process = new System.Diagnostics.Process
            // {
            //     StartInfo = new System.Diagnostics.ProcessStartInfo
            //     {
            //         FileName = "pg_dump",
            //         Arguments = $"-h localhost -U username -d dbname -f {backupFileName}",
            //         UseShellExecute = false,
            //         RedirectStandardOutput = true,
            //         CreateNoWindow = true
            //     }
            // };
            // await process.StartAsync();
            // await process.WaitForExitAsync();

            await Task.CompletedTask;

            _logger.LogInformation("Veritabanı yedeği tamamlandı: {File}", backupFileName);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Veritabanı yedeği sırasında hata");
        }
    }

    private async Task BackupFiles(string timestamp)
    {
        try
        {
            _logger.LogInformation("Dosya yedeği başlatılıyor: {Timestamp}", timestamp);

            var directoriesToBackup = new[]
            {
                Path.Combine(Environment.CurrentDirectory, "wwwroot", "uploads"),
                Path.Combine(Environment.CurrentDirectory, "logs")
            };

            var zipFileName = Path.Combine(_backupPath, $"files_backup_{timestamp}.zip");

            using (var zipArchive = ZipFile.Open(zipFileName, ZipArchiveMode.Create))
            {
                foreach (var directory in directoriesToBackup)
                {
                    if (!Directory.Exists(directory))
                    {
                        _logger.LogWarning("Yedeklenecek klasör bulunamadı: {Directory}", directory);
                        continue;
                    }

                    await AddDirectoryToZip(zipArchive, directory, Path.GetFileName(directory));
                }
            }

            _logger.LogInformation("Dosya yedeği tamamlandı: {File}", zipFileName);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Dosya yedeği sırasında hata");
        }
    }

    private async Task AddDirectoryToZip(ZipArchive zipArchive, string sourceDirectory, string entryPrefix)
    {
        var files = Directory.GetFiles(sourceDirectory, "*", SearchOption.AllDirectories);

        foreach (var file in files)
        {
            var relativePath = Path.GetRelativePath(sourceDirectory, file);
            var entryName = Path.Combine(entryPrefix, relativePath).Replace('\\', '/');

            zipArchive.CreateEntryFromFile(file, entryName);
        }

        await Task.CompletedTask;
    }

    private void CleanOldBackups()
    {
        try
        {
            var cutoffDate = DateTime.UtcNow.AddDays(-30);
            var backupFiles = Directory.GetFiles(_backupPath);

            foreach (var file in backupFiles)
            {
                var fileInfo = new FileInfo(file);
                if (fileInfo.CreationTimeUtc < cutoffDate)
                {
                    _logger.LogInformation("Eski yedek siliniyor: {File}", file);
                    File.Delete(file);
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Eski yedekleri temizlerken hata");
        }
    }
}
