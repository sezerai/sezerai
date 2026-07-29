using SezerAiWeb.Application.DTOs;

namespace SezerAiWeb.Application.Interfaces;

public interface IPlatformStatsService
{
    Task<PlatformUserCountDto> GetPlatformUserCountsAsync();
}
