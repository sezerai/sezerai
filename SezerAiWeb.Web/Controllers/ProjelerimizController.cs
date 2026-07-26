using Microsoft.AspNetCore.Mvc;
using SezerAiWeb.Application.DTOs;
using SezerAiWeb.Application.Interfaces;

namespace SezerAiWeb.Web.Controllers;

[Route("projelerimiz")]
public class ProjelerimizController(IProjeServisi projeServisi, IDemoTalebiServisi demoTalebiServisi, ISeoService seoService) : Controller
{
    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        ViewData["Seo"] = seoService.GetProjelerimizIndexSeo();
        var projeler = await projeServisi.TumProjeleriGetirAsync();
        return View(projeler);
    }

    [HttpGet("{slug}")]
    public async Task<IActionResult> Detay(string slug)
    {
        var proje = await projeServisi.SlugIleGetirAsync(slug);
        if (proje is null) return NotFound();

        ViewData["Seo"] = seoService.GetProjelerimizDetaySeo(proje);
        return View(proje);
    }

    [HttpPost("demo-talep")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DemoTalep(DemoTalebiFormDto form, string donusSlug)
    {
        var projeId = await projeServisi.IdSlugIleGetirAsync(donusSlug);
        if (projeId is null)
        {
            return NotFound();
        }

        form.ProjeId = projeId.Value;
        ModelState.Remove(nameof(DemoTalebiFormDto.ProjeId));

        if (!ModelState.IsValid)
        {
            TempData["DemoHata"] = "Lütfen zorunlu alanları eksiksiz doldurun.";
            return RedirectToAction(nameof(Detay), new { slug = donusSlug });
        }

        await demoTalebiServisi.GonderAsync(form);
        TempData["DemoBasarili"] = "Demo talebiniz alındı, en kısa sürede sizinle iletişime geçeceğiz.";
        return RedirectToAction(nameof(Detay), new { slug = donusSlug });
    }
}
