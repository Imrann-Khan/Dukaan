namespace Dukaan.Application.Dtos;

public record RegisterRequestDTO
(
    string Username,
    string Email,
    string Password,
    string StoreName,
    string StoreSlug
);