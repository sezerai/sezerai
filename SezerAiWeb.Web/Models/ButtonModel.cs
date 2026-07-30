namespace SezerAiWeb.Web.Models;

public class ButtonModel
{
    public string Metin { get; set; } = null!;
    public string Href { get; set; } = null!;

    // dolu-lacivert, dolu-altin, dis-hat-beyaz, dis-hat-lacivert
    public string Varyant { get; set; } = "dolu-lacivert";
}
