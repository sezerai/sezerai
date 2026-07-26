using AutoMapper;
using SezerAiWeb.Application.DTOs;
using SezerAiWeb.Domain.Entities;

namespace SezerAiWeb.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Website Mappings
        CreateMap<Website, WebsiteDto>().ReverseMap();
        CreateMap<WebsiteCreateDto, Website>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());
        CreateMap<WebsiteUpdateDto, Website>()
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

        // Menu Mappings
        CreateMap<WebsiteMenu, MenuDto>()
            .ForMember(dest => dest.Children, opt => opt.MapFrom(src => src.Children));
        CreateMap<MenuCreateDto, WebsiteMenu>();
        CreateMap<MenuUpdateDto, WebsiteMenu>();

        // User Mappings
        CreateMap<User, UserDto>()
            .ForMember(dest => dest.Roles, opt => opt.MapFrom(src =>
                src.UserRoles.Select(ur => ur.Role.Name).ToList()));

        // AlertNotification Mappings
        CreateMap<AlertNotification, AlertDto>()
            .ForMember(dest => dest.WebsiteName, opt => opt.MapFrom(src => src.Website != null ? src.Website.Name : null));

        // AIAgentLog Mappings
        CreateMap<AIAgentLog, AIAgentDto>();

        // SiteMetrics Mappings
        CreateMap<SiteMetrics, DashboardMetricsDto>();

        // SystemHealth Mappings
        CreateMap<SystemHealth, SiteHealthDto>();

        // Blog Mappings (existing)
        CreateMap<BlogYazisi, BlogKartDto>();
        CreateMap<BlogYazisi, BlogDetayDto>();
    }
}
