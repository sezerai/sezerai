namespace SezerAiWeb.Application.Common;

public class SeoMeta
{
    public string Baslik { get; set; } = "SEZER AI Technology - Yapay Zeka Çözümleri | Shaping the Future";
    public string Aciklama { get; set; } =
        "SEZER AI Technology, 2019'dan bu yana sağlık sektöründe yapay zeka destekli Ar-Ge ve inovasyon yürüten SETINT AI Robotic Sistem Eğitim ve Danışmanlık A.Ş. bünyesindeki teknoloji markasıdır.";
    public string AnahtarKelimeler { get; set; } = "SEZER AI, yapay zeka, sağlık teknolojisi, robot hemşire, AI Hospital";
    public string CanonicalYol { get; set; } = "/";
    public string OgGorsel { get; set; } = "/images/logo-sezerai.png";
    public string OgTip { get; set; } = "website";
    public List<BreadcrumbOgesi> Breadcrumb { get; set; } = [];
    public List<string> EkJsonLd { get; set; } = [];
}

public class BreadcrumbOgesi
{
    public string Ad { get; set; } = null!;
    public string? Yol { get; set; }
}
