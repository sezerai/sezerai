namespace SezerAiWeb.Application.DTOs;

public class DashboardMetricsDto
{
    public int TotalWebsites { get; set; }
    public int ActiveWebsites { get; set; }
    public int InactiveWebsites { get; set; }
    public int TotalBlogs { get; set; }
    public int PublishedBlogs { get; set; }
    public int DraftBlogs { get; set; }
    public int TotalMenuItems { get; set; }
    public int TotalProjects { get; set; }
    public int ActiveProjects { get; set; }
    public DateTime LastUpdated { get; set; }
}
