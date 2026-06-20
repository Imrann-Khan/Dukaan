namespace Dukaan.Application.Dtos;

public record AuthResponseDTO(
    string Token,
    string UserName,
    Guid TenantId
);