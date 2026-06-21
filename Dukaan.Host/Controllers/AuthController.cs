using Dukaan.Application.Features.Auth.Commands.Login;
using Dukaan.Application.Features.Auth.Commands.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;
namespace Dukaan.Host.Controllers;

[ApiController]
[Route("/[controller]")]
public class AuthController(ISender sender) : ControllerBase
{
    // Post register
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody]RegisterCommand command)
    {
        try
        {
            var response = await sender.Send(command);
            return Ok(response);
        }
        catch(Exception err)
        {
            return BadRequest(new {error = err.Message});
        }
    }

    // Post Login]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody]LoginCommand command)
    {
        try
        {
            var response = await sender.Send(command);
            return Ok(response);
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized(new { error = "Invalid email or password" });
        }
    }
}