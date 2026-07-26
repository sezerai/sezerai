using SezerAiWeb.Application.DTOs;

namespace SezerAiWeb.Application.Interfaces;

public interface IIletisimServisi
{
    Task GonderAsync(IletisimFormDto dto);
}
