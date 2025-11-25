using Domains.Entities;
using Domains.ValueObjects;

namespace Applications.Interfaces;

public interface IUserRepository
{
    Task<User?> GetUserByIdAsync(UserId userId);
    Task AddUserAsync(User user);
    Task UpdateUserAsync(User user);
    Task <User?> GetUserByUsernameAsync(Username username);
}