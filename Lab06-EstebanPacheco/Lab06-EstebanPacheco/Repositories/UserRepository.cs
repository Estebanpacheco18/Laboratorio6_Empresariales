using Lab06_EstebanPacheco.Models;
using Lab06_EstebanPacheco.Repositories.Interfaces;

namespace Lab06_EstebanPacheco.Repositories;

public class UserRepository : IUserRepository
{
    private readonly List<User> _users = new()
    {
        new User { Id = 1, Username = "admin", Password = "123", Role = "Admin" },
        new User { Id = 2, Username = "user", Password = "123", Role = "User" }
    };

    public Task<IEnumerable<User>> GetAllAsync() => Task.FromResult(_users.AsEnumerable());

    public Task<User> GetByIdAsync(int id) => Task.FromResult(_users.FirstOrDefault(u => u.Id == id));

    public Task AddAsync(User entity)
    {
        _users.Add(entity);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(User entity) => Task.CompletedTask;

    public Task DeleteAsync(int id)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);
        if (user != null) _users.Remove(user);
        return Task.CompletedTask;
    }

    public Task<User> GetByUsernameAsync(string username) =>
        Task.FromResult(_users.FirstOrDefault(u => u.Username == username));
}
