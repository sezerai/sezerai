using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SezerAiWeb.Application.Common;
using SezerAiWeb.Application.Interfaces;
using SezerAiWeb.Web.Models;

namespace SezerAiWeb.Web.Controllers;

public class HomeController(IProjeServisi projeServisi, IBlogServisi blogServisi) : Controller
{
    [Route("/")]
    public async Task<IActionResult> Index()
    {
        ViewData["Seo"] = new SeoMeta
        {
            Baslik = "SEZER AI Technology - Yapay Zeka Çözümleri | Shaping the Future",
            Aciklama = "SEZER AI Technology, 2019'dan bu yana sağlık sektöründe yapay zeka destekli Ar-Ge ve inovasyon yürüten SETINT AI Robotic Sistem Eğitim ve Danışmanlık A.Ş. bünyesindeki teknoloji markasıdır.",
            AnahtarKelimeler = "SEZER AI, yapay zeka, sağlık teknolojisi, robot hemşire, AI Hospital, AIBazaar",
            CanonicalYol = "/",
        };

        var projeler = await projeServisi.TumProjeleriGetirAsync();
        var yaziLar = await blogServisi.TumYazilariGetirAsync();

        var model = new AnaSayfaViewModel
        {
            Projeler = projeler,
            SonBlogYazilari = yaziLar.Take(3).ToList(),
        };

        return View(model);
    }

    [Route("/hakkimizda")]
    public IActionResult Hakkimizda()
    {
        ViewData["Seo"] = new SeoMeta
        {
            Baslik = "Hakkımızda - SEZER AI Technology",
            Aciklama = "SETINT AI Robotic Sistem Eğitim ve Danışmanlık A.Ş., 2019 yılında Düzce Üniversitesi Teknopark bünyesinde kuruldu. Sağlık sektöründe yapay zeka destekli Ar-Ge ve inovasyon yürütüyoruz.",
            AnahtarKelimeler = "SEZER AI hakkında, SETINT AI Robotic, Düzce Teknopark, yapay zeka Ar-Ge",
            CanonicalYol = "/hakkimizda",
            Breadcrumb =
            [
                new BreadcrumbOgesi { Ad = "Anasayfa", Yol = "/" },
                new BreadcrumbOgesi { Ad = "Hakkımızda" },
            ],
        };

        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
