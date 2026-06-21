namespace Dukaan.Application.Features.Auth.Commands.Login;

using MediatR;
using Dukaan.Application.Dtos;
using Microsoft.AspNetCore.Identity;
using Dukaan.Domain.Data.Model;
using Dukaan.Application.Services;
using Microsoft.Extensions.Configuration;
using Dukaan.Domain.Entities;

public class LoginCommandHandler(UserManager<Merchant> userManager, TenantService tenantService, AuthService authService) : IRequestHandler<LoginCommand, AuthResponseDTO>
{
    public async Task<AuthResponseDTO> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if(user == null || !await userManager.CheckPasswordAsync(user, request.Password))
        {
            throw new Exception("Invalid credentials");
        }       

        var Token = authService.GenerateJwtToken(user);

        return new AuthResponseDTO(Token, user.UserName!, user.TenantId);
    }
}