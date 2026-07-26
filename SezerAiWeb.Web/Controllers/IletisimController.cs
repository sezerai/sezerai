using Microsoft.AspNetCore.Mvc;
using SezerAiWeb.Application.DTOs;
using SezerAiWeb.Application.Interfaces;

namespace SezerAiWeb.Web.Controllers;

[Route("iletisim")]
public class IletisimController(IIletisimServisi iletisimServisi, ISeoService seoService) : Controller
{
    [HttpGet("")]
    public IActionResult Index()
    {
        ViewData["Seo"] = seoService.GetIletisimSeo();
        return View(new IletisimFormDto());
    }

    [HttpPost("")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(IletisimFormDto form)
    {
        if (!ModelState.IsValid)
        {
            ViewData["Seo"] = seoService.GetIletisimHataliSeo();
            return View(form);
        }

        await iletisimServisi.GonderAsync(form);
        TempData["IletisimBasarili"] = "Mesajınız alındı, en kısa sürede size dönüş yapacağız.";
        return RedirectToAction(nameof(Index));
    }
}
