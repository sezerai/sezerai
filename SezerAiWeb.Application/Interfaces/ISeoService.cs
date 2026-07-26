using SezerAiWeb.Application.Common;
using SezerAiWeb.Application.DTOs;

namespace SezerAiWeb.Application.Interfaces;

public interface ISeoService
{
    SeoMeta GetProjelerimizIndexSeo();
    SeoMeta GetProjelerimizDetaySeo(ProjeDetayDto proje);
    SeoMeta GetBlogIndexSeo();
    SeoMeta GetBlogDetaySeo(BlogDetayDto yazi);
    SeoMeta GetIletisimSeo();
    SeoMeta GetIletisimHataliSeo();
}
