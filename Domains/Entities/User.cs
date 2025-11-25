using Domains.ValueObjects;

namespace Domains.Entities;

public class User
{
    private User(){}
    private UserId _id;
    public UserId Id => _id;
    private Username _username;
    public Username Username => _username;
    private PasswordHash _passwordHash;
    public PasswordHash PasswordHash => _passwordHash;
    public User(Username username, PasswordHash passwordHash)
    {
        _passwordHash = passwordHash;
        _username = username;
        _id = UserId.New();
    }
}