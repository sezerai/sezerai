namespace SezerAiWeb.Application.DTOs;

public class BlogKartDto
{
    public string Baslik { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string Ozet { get; set; } = null!;
    public string KapakGorseli { get; set; } = null!;
    public string Yazar { get; set; } = null!;
    public DateTime YayinTarihi { get; set; }
}
