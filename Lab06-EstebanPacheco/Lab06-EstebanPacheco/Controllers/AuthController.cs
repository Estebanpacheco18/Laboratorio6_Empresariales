using Lab06_EstebanPacheco.DTOs;
using Lab06_EstebanPacheco.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lab06_EstebanPacheco.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequestDto dto)
    {
        var token = _userService.Authenticate(dto.Username, dto.Password);
        if (token == null) return Unauthorized("Credenciales inválidas");
        return Ok(new { Token = token });
    }
}