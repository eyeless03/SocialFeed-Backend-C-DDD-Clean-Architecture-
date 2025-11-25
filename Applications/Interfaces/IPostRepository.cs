using Domains.Entities;
using Domains.ValueObjects;

namespace Applications.Interfaces;

public interface IPostRepository
{
    Task<Post?> GetPostByIdAsync(PostId postId);
    Task AddPostAsync(Post post);
    Task UpdatePostAsync(Post post);
}