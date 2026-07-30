using Microsoft.AspNetCore.Mvc;
using SezerAiWeb.Application.Interfaces;

namespace SezerAiWeb.Web.Areas.MasterPanel.Controllers
{
    [Area("MasterPanel")]
    public class DashboardController : Controller
    {
        private readonly IPlatformStatsService _platformStatsService;

        public DashboardController(IPlatformStatsService platformStatsService)
        {
            _platformStatsService = platformStatsService;
        }

        public async Task<IActionResult> Index()
        {
            var platformStats = await _platformStatsService.GetPlatformUserCountsAsync();
            return View(platformStats);
        }
    }
}
