using Applications.Interfaces;
using Domains.Entities;
using Domains.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace WebApplication3.Infrastructure.Repositories;

public class PostRepository:IPostRepository
{
    private readonly AppDbContext _dbContext;

    public PostRepository(AppDbContext context)
    {
        _dbContext = context;
    }
    
    public async Task<Post?> GetPostByIdAsync(PostId postId)
    {
        return await _dbContext.Posts.Include(p => p.Comments).FirstOrDefaultAsync(p => p.Id == postId);
    }

    public async Task AddPostAsync(Post post)
    {
        await _dbContext.Posts.AddAsync(post);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdatePostAsync(Post post)
    {
        _dbContext.Update(post);
        await _dbContext.SaveChangesAsync();
    }
}