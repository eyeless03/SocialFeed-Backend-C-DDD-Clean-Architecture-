using Applications.Interfaces;
using Domains.Entities;
using Domains.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace WebApplication3.Infrastructure.Repositories;

public class UserRepository:IUserRepository
{
    private readonly AppDbContext _dbContext;
    public UserRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User?> GetUserByIdAsync(UserId userId)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(id => id.Id == userId); 
    }

    public async Task AddUserAsync(User user)
    {
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateUserAsync(User user)
    {
        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<User?> GetUserByUsernameAsync(Username username)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(id => id.Username == username);
    }
}