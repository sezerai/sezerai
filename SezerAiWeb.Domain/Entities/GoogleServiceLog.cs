using SezerAiWeb.Domain.Common;

namespace SezerAiWeb.Domain.Entities;

public class GoogleServiceLog : BaseEntity
{
    public string ServiceName { get; set; } = null!; // Analytics, SearchConsole, TagManager
    public string ActionType { get; set; } = null!; // Fetch, Update, Sync
    public string? RequestData { get; set; } // JSON format
    public string? ResponseData { get; set; } // JSON format
    public bool IsSuccess { get; set; } = false;
    public string? ErrorMessage { get; set; }
    public int? StatusCode { get; set; }
    public TimeSpan? Duration { get; set; }
    public Guid? WebsiteId { get; set; }
    public Website? Website { get; set; }
}
