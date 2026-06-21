namespace Dukaan.Application.Features.Auth.Commands.Login;

using MediatR;
using Dukaan.Application.Dtos;
public record LoginCommand(
    string Email,
    string Password
) : IRequest<AuthResponseDTO>;