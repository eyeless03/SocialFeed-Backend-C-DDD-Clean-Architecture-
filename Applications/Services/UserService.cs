using Applications.Dtos;
using Applications.Interfaces;
using Domains.Entities;
using Domains.ValueObjects;

namespace Applications.Services;

public class UserService: IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    public UserService(IUserRepository userRepository,IPasswordHasher passwordHasher)
    {
        _passwordHasher = passwordHasher;
        _userRepository = userRepository;
    }

    public async Task<UserId> CreateUserAsync(Username username, string passwordHash)
    {
        var password = _passwordHasher.HashPassword(passwordHash);
        var user = new User(username, password);
        await _userRepository.AddUserAsync(user);
        return user.Id;
    }

    public async Task<UserDto?> GetUserByIdAsync(UserId userId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);
        if (user == null)
        {
            return null;
        }
        return new UserDto(user.Id.Value, new Username(user.Username.Value));
    }

    public async Task<UserDto?> Authentication(Username username, string password)
    {
        var user = await _userRepository.GetUserByUsernameAsync(username);
        if (user == null)
        {
            return null;
        }

        if (!_passwordHasher.VerifyHashedPassword(password, user.PasswordHash))
        {
            return null;
        }
        return new UserDto(user.Id.Value, new Username(user.Username.Value));
    }
}