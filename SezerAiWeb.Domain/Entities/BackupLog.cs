using SezerAiWeb.Domain.Common;

namespace SezerAiWeb.Domain.Entities;

public class BackupLog : BaseEntity
{
    public string BackupType { get; set; } = null!; // Database, Files, Full
    public DateTime BackupStartedAt { get; set; }
    public DateTime? BackupCompletedAt { get; set; }
    public bool IsSuccess { get; set; } = false;
    public string? ErrorMessage { get; set; }
    
    // Backup Details
    public string? BackupLocation { get; set; }
    public long? BackupSize { get; set; } // bytes
    public string? BackupFileName { get; set; }
    public string BackupMethod { get; set; } = "Automatic"; // Automatic, Manual
    
    // Restore Info
    public bool CanRestore { get; set; } = true;
    public DateTime? ExpiresAt { get; set; }
    public string? ChecksumHash { get; set; } // For integrity verification
    
    public string? AdditionalDataJson { get; set; } // JSON for extensibility
}
