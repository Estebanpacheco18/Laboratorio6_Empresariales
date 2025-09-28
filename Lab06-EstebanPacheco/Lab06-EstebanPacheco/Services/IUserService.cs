using Lab06_EstebanPacheco.DTOs;

namespace Lab06_EstebanPacheco.Services;
public interface IUserService
{
    IEnumerable<UserDto> GetAllUsers();
    UserDto CreateUser(UserDto userDto);
    bool DeleteUser(int id);
    string Authenticate(string username, string password);
    UserDto GetUserById(int id);
}
