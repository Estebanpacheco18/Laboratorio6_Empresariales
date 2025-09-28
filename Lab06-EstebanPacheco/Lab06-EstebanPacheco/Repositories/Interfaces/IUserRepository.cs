using Lab06_EstebanPacheco.Models;

namespace Lab06_EstebanPacheco.Repositories.Interfaces;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User> GetByUsernameAsync(string username);
}
