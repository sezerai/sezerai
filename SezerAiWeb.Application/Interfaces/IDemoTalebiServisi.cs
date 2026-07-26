using SezerAiWeb.Application.DTOs;

namespace SezerAiWeb.Application.Interfaces;

public interface IDemoTalebiServisi
{
    Task GonderAsync(DemoTalebiFormDto dto);
}
