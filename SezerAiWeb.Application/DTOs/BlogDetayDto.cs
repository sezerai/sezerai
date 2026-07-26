namespace SezerAiWeb.Application.DTOs;

public class BlogDetayDto
{
    public string Baslik { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string Ozet { get; set; } = null!;
    public string IcerikHtml { get; set; } = null!;
    public string KapakGorseli { get; set; } = null!;
    public string Yazar { get; set; } = null!;
    public DateTime YayinTarihi { get; set; }
    public string MetaBaslik { get; set; } = null!;
    public string MetaAciklama { get; set; } = null!;
    public string MetaAnahtarKelimeler { get; set; } = null!;
}
