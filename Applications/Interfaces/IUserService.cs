using Applications.Dtos;
using Domains.Entities;
using Domains.ValueObjects;

namespace Applications.Interfaces;

public interface IUserService
{
    Task<UserId> CreateUserAsync(Username username, string passwordHash);
    Task<UserDto?> GetUserByIdAsync(UserId userId); 
    Task<UserDto?> Authentication(Username username, string password);
}