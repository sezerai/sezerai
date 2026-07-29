namespace SezerAiWeb.Application.DTOs;

public class PlatformUserCountDto
{
    public int GeliyooUserCount { get; set; }
    public int AiHospitalUserCount { get; set; }
    public int AiBazaarUserCount { get; set; }
    public int PerdeImalatUserCount { get; set; }
    public DateTime LastUpdated { get; set; }
}
