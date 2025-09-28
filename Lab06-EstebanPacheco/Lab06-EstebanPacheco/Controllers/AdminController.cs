namespace Lab06_EstebanPacheco.Controllers;

using DTOs;
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
    public IActionResult CreateUser([FromBody] UserDto userDto)
    {
        var user = _userService.CreateUser(userDto);
        return CreatedAtAction(nameof(GetAllUsers), new { id = user.Id }, user);
    }

    [HttpDelete("users/{id}")]
    public IActionResult DeleteUser(int id)
    {
        var result = _userService.DeleteUser(id);
        if (!result) return NotFound("Usuario no encontrado");
        return NoContent();
    }
}