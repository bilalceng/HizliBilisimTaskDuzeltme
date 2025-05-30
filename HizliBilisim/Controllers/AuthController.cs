using HizliBilisim.DTOs;
using HizliBilisim.Mappers;
using HizliBilisim.models;
using HizliBilisim.services.interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HizliBilisim.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IJwtTokenService _jwtTokenService;

    public AuthController(IAuthService authService, IJwtTokenService jwtTokenService)
    {
        _authService = authService;
        _jwtTokenService = jwtTokenService;
    }

    [HttpPost("login")] 
    public async Task<IActionResult> Login([FromBody] UserForCreationDto dto)
    {
        var userDto = await _authService.LoginAsync(dto.UserName, dto.Password);
        if (userDto == null)
            return Unauthorized("Invalid credentials");

        var token = _jwtTokenService.GenerateToken(userDto.ToEntity());

        return Ok(new
        {
            user = userDto,
            token
        });
    }
}
