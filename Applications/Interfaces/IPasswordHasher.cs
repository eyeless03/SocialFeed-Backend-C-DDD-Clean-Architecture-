using Domains.ValueObjects;

namespace Applications.Interfaces;

public interface IPasswordHasher
{
    PasswordHash HashPassword(string password);
    bool VerifyHashedPassword(string password,PasswordHash passwordHash);
}