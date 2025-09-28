using Lab06_EstebanPacheco.DTOs;
using Lab06_EstebanPacheco.Models;

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
            Id = dto.Id,
            Username = dto.Username,
            Role = dto.Role,
            Password = dto.Password // Asignar la contraseña
        };

        _userService.CreateUser(user);

        return Ok("Usuario creado exitosamente.");
    }

    [HttpDelete("users/{id}")]
    public IActionResult DeleteUser(int id)
    {
        var result = _userService.DeleteUser(id);
        if (!result) return NotFound("Usuario no encontrado");
        return NoContent();
    }
}