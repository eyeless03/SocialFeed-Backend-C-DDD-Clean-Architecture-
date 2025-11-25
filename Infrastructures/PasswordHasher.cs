using Applications.Interfaces;
using Domains.ValueObjects;
namespace WebApplication3.Infrastructure;

public class PasswordHasher: IPasswordHasher
{
    public PasswordHash HashPassword(string password) => new PasswordHash(BCrypt.Net.BCrypt.HashPassword(password));

    public bool VerifyHashedPassword(string password,PasswordHash passwordHash)
    {
        return BCrypt.Net.BCrypt.Verify(password, passwordHash.Value);
    }
}