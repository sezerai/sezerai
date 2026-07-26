using SezerAiWeb.Application.Common;
using SezerAiWeb.Application.DTOs;
using SezerAiWeb.Application.Interfaces;

namespace SezerAiWeb.Application.Services;

public class SeoService : ISeoService
{
    public SeoMeta GetProjelerimizIndexSeo()
    {
        return new SeoMeta
        {
            Baslik = "Projelerimiz - Robot Hemşire, AI Hospital, AI Freeluncher, AIBazaar | SEZER AI Technology",
            Aciklama = "SEZER AI Technology'nin yapay zeka teknolojileriyle geliştirdiği Robot Hemşire, AI Hospital, AI Freeluncher ve AIBazaar projelerini keşfedin.",
            AnahtarKelimeler = "SEZER AI projeleri, Robot Hemşire, AI Hospital, AI Freeluncher, AIBazaar",
            CanonicalYol = "/projelerimiz",
            Breadcrumb =
            [
                new BreadcrumbOgesi { Ad = "Anasayfa", Yol = "/" },
                new BreadcrumbOgesi { Ad = "Projelerimiz" },
            ],
        };
    }

    public SeoMeta GetProjelerimizDetaySeo(ProjeDetayDto proje)
    {
        return new SeoMeta
        {
            Baslik = proje.MetaBaslik,
            Aciklama = proje.MetaAciklama,
            AnahtarKelimeler = proje.MetaAnahtarKelimeler,
            CanonicalYol = $"/projelerimiz/{proje.Slug}",
            OgGorsel = proje.BannerGorseli,
            OgTip = "article",
            Breadcrumb =
            [
                new BreadcrumbOgesi { Ad = "Anasayfa", Yol = "/" },
                new BreadcrumbOgesi { Ad = "Projelerimiz", Yol = "/projelerimiz" },
                new BreadcrumbOgesi { Ad = proje.Ad },
            ],
        };
    }

    public SeoMeta GetBlogIndexSeo()
    {
        return new SeoMeta
        {
            Baslik = "Blog - SEZER AI Technology",
            Aciklama = "SEZER AI Technology'den yapay zeka ve sağlık teknolojileri alanında güncel yazılar ve haberler.",
            AnahtarKelimeler = "SEZER AI blog, yapay zeka haberleri, sağlık teknolojisi yazıları",
            CanonicalYol = "/blog",
            Breadcrumb =
            [
                new BreadcrumbOgesi { Ad = "Anasayfa", Yol = "/" },
                new BreadcrumbOgesi { Ad = "Blog" },
            ],
        };
    }

    public SeoMeta GetBlogDetaySeo(BlogDetayDto yazi)
    {
        return new SeoMeta
        {
            Baslik = yazi.MetaBaslik,
            Aciklama = yazi.MetaAciklama,
            AnahtarKelimeler = yazi.MetaAnahtarKelimeler,
            CanonicalYol = $"/blog/{yazi.Slug}",
            OgGorsel = yazi.KapakGorseli,
            OgTip = "article",
            Breadcrumb =
            [
                new BreadcrumbOgesi { Ad = "Anasayfa", Yol = "/" },
                new BreadcrumbOgesi { Ad = "Blog", Yol = "/blog" },
                new BreadcrumbOgesi { Ad = yazi.Baslik },
            ],
        };
    }

    public SeoMeta GetIletisimSeo()
    {
        return new SeoMeta
        {
            Baslik = "İletişim - SEZER AI Technology",
            Aciklama = "SEZER AI Technology ile iletişime geçin. Telefon: +90 541 195 53 04. Düzce Üniversitesi Teknopark, Düzce.",
            AnahtarKelimeler = "SEZER AI iletişim, Düzce Teknopark, yapay zeka şirketi iletişim",
            CanonicalYol = "/iletisim",
            Breadcrumb =
            [
                new BreadcrumbOgesi { Ad = "Anasayfa", Yol = "/" },
                new BreadcrumbOgesi { Ad = "İletişim" },
            ],
        };
    }

    public SeoMeta GetIletisimHataliSeo()
    {
        return new SeoMeta
        {
            Baslik = "İletişim - SEZER AI Technology",
            CanonicalYol = "/iletisim"
        };
    }
}
