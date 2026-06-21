namespace Dukaan.Application.Features.Auth.Commands.Register;

using MediatR;
using Dukaan.Application.Dtos;
public record RegisterCommand(
    string Username,
    string Email,
    string Password,
    string StoreName,
    string StoreSlug
) : IRequest<AuthResponseDTO>;