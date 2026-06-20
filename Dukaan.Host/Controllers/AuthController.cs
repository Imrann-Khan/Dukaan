using Dukaan.Application.Dtos;
using Dukaan.Application.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/[controller]")]
public class AuthController(AuthService authService) : ControllerBase
{
    // Post register
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody]RegisterRequestDTO request)
    {
        try
        {
            var response = await authService.RegisterAsync(request);
            return Ok(response);
        }
        catch(Exception err)
        {
            return BadRequest(new {error = err.Message});
        }
    }

    // Post Login]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody]LoginRequestDTO request)
    {
        try
        {
            var response = await authService.LoginAsync(request);
            return Ok(response);
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized(new { error = "Invalid email or password" });
        }
    }
}