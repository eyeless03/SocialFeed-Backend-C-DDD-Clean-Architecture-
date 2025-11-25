using Applications.Dtos;

namespace Applications.Interfaces;

public interface IJWTService
{
    string GenerateJWTToken(UserDto user);
}