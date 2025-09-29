using Lab06_EstebanPacheco.DTOs;

namespace Lab06_EstebanPacheco.Controllers;

using Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AdminController : ControllerBase
{
    private readonly IUserService _userService;

    public AdminController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("users")]
    public IActionResult GetAllUsers()
    {
        var users = _userService.GetAllUsers();
        return Ok(users);
    }

    [HttpPost("users")]
    public IActionResult CreateUser([FromBody] UserRequestDto dto)
    {
        if (string.IsNullOrEmpty(dto.Password))
        {
            return BadRequest("La contraseña es obligatoria.");
        }

        var user = new UserDto
        {
            Username = dto.Username,
            Role = dto.Role,
            Password = dto.Password // Asignar la contraseña
        };

        var createdUser = _userService.CreateUser(user);

        return Ok(createdUser);
    }

    [HttpDelete("users/{id}")]
    public IActionResult DeleteUser(int id)
    {
        var result = _userService.DeleteUser(id);
        if (!result) return NotFound("Usuario no encontrado");
        return NoContent();
    }
}