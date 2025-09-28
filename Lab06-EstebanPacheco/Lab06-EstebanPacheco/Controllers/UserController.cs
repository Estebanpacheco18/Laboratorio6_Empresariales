namespace Lab06_EstebanPacheco.Controllers;

using Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{id}")]
    public IActionResult GetUser(int id)
    {
        var userDto = _userService.GetUserById(id);
        if (userDto == null) return NotFound("Usuario no encontrado");

        return Ok(userDto);
    }
}