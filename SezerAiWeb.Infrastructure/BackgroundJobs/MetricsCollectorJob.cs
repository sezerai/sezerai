using Microsoft.Extensions.Logging;

namespace SezerAiWeb.Infrastructure.BackgroundJobs;

/// <summary>
/// Sistem metriklerini toplamak için periyodik job
/// </summary>
public class MetricsCollectorJob
{
    private readonly ILogger<MetricsCollectorJob> _logger;

    public MetricsCollectorJob(ILogger<MetricsCollectorJob> logger)
    {
        _logger = logger;
    }

    public async Task Execute()
    {
        _logger.LogInformation("MetricsCollectorJob başlatıldı: {Time}", DateTime.UtcNow);

        try
        {
            var metrics = await CollectMetrics();

            _logger.LogInformation("Toplanan metrikler: {@Metrics}", metrics);

            // TODO: Metrikleri veritabanına kaydet veya monitoring sistemine gönder
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "MetricsCollectorJob sırasında hata oluştu");
        }
    }

    private async Task<SystemMetrics> CollectMetrics()
    {
        await Task.CompletedTask;

        var currentProcess = System.Diagnostics.Process.GetCurrentProcess();

        return new SystemMetrics
        {
            Timestamp = DateTime.UtcNow,
            CpuUsagePercent = GetCpuUsage(),
            MemoryUsageMB = currentProcess.WorkingSet64 / 1024 / 1024,
            ThreadCount = currentProcess.Threads.Count,
            HandleCount = currentProcess.HandleCount,
            GCGen0Collections = GC.CollectionCount(0),
            GCGen1Collections = GC.CollectionCount(1),
            GCGen2Collections = GC.CollectionCount(2)
        };
    }

    private double GetCpuUsage()
    {
        try
        {
            var currentProcess = System.Diagnostics.Process.GetCurrentProcess();
            var startTime = DateTime.UtcNow;
            var startCpuUsage = currentProcess.TotalProcessorTime;

            // Kısa bir bekleme
            System.Threading.Thread.Sleep(500);

            var endTime = DateTime.UtcNow;
            var endCpuUsage = currentProcess.TotalProcessorTime;

            var cpuUsedMs = (endCpuUsage - startCpuUsage).TotalMilliseconds;
            var totalMsPassed = (endTime - startTime).TotalMilliseconds;
            var cpuUsageTotal = cpuUsedMs / (Environment.ProcessorCount * totalMsPassed);

            return cpuUsageTotal * 100;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "CPU kullanımı hesaplanırken hata");
            return 0;
        }
    }
}

public class SystemMetrics
{
    public DateTime Timestamp { get; set; }
    public double CpuUsagePercent { get; set; }
    public long MemoryUsageMB { get; set; }
    public int ThreadCount { get; set; }
    public int HandleCount { get; set; }
    public int GCGen0Collections { get; set; }
    public int GCGen1Collections { get; set; }
    public int GCGen2Collections { get; set; }
}
