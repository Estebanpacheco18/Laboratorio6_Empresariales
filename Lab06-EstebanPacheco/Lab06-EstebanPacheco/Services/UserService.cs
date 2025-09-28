using Lab06_EstebanPacheco.DTOs;
using Lab06_EstebanPacheco.Models;
using Lab06_EstebanPacheco.Repositories.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Lab06_EstebanPacheco.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public string Authenticate(string username, string password)
    {
        var user = _userRepository.GetByUsernameAsync(username).Result;
        if (user == null || user.Password != password) return null;

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("claveSuperSegura123456789012345678901234567890"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "MiApp",
            audience: "MiAppUser",
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public IEnumerable<UserDto> GetAllUsers()
    {
        return _userRepository.GetAllAsync().Result.Select(user => new UserDto
        {
            Id = user.Id,
            Username = user.Username,
            Role = user.Role,
            Password = user.Password
        });
    }

    public UserDto CreateUser(UserDto userDto)
    {
        var user = new User
        {
            Username = userDto.Username,
            Role = userDto.Role,
            Password = "defaultPassword" // Contraseña predeterminada
        };

        _userRepository.AddAsync(user).Wait();
        return new UserDto
        {
            Id = user.Id,
            Username = user.Username,
            Role = user.Role,
            Password = user.Password
        };
    }

    public bool DeleteUser(int id)
    {
        var user = _userRepository.GetByIdAsync(id).Result;
        if (user == null) return false;

        _userRepository.DeleteAsync(id).Wait();
        return true;
    }

    public UserDto GetUserById(int id)
    {
        var user = _userRepository.GetByIdAsync(id).Result;
        if (user == null) return null;

        return new UserDto
        {
            Id = user.Id,
            Username = user.Username,
            Role = user.Role,
            Password = user.Password
        };
    }
}
