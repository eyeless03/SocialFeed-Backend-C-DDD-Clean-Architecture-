using Applications.Interfaces;
using Domains.Entities;
using Domains.ValueObjects;

namespace Applications.Services;

public class PostService:IPostService
{
    private readonly IPostRepository _postRepository;

    public PostService(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }
    
    public async Task<PostId> CreatePostAsync(PostText text, UserId userId)
    {
        var post = new Post(text, userId);
        await _postRepository.AddPostAsync(post);
        return post.Id;
    }

    public async Task<CommentId> AddCommentAsync(PostId postId,UserId userId, CommentText text)
    {
        var post = await _postRepository.GetPostByIdAsync(postId);
        if (post == null)
        {
            throw new Exception("Post not found");
        }

        var comment = post.AddComment(text, userId);
        await _postRepository.UpdatePostAsync(post);
        return comment.Id;
    }

    public async Task AddLikeOnPostAsync(PostId postId, UserId userId)
    {
        var post = await _postRepository.GetPostByIdAsync(postId);
        if (post == null)
        {
            throw new Exception("Post not found");
        }
        post.AddLike(userId);
        await _postRepository.UpdatePostAsync(post);
    }

    public async Task RemoveLikeFromPostAsync(PostId postId, UserId userId)
    {
        var post = await _postRepository.GetPostByIdAsync(postId);
        if (post == null)
        {
            throw new Exception("Post not found");
        }
        post.RemoveLike(userId);
        await _postRepository.UpdatePostAsync(post);
    }

    public async Task AddDislikeOnPostAsync(PostId postId, UserId userId)
    {
        var post = await _postRepository.GetPostByIdAsync(postId);
        if (post == null)
        {
            throw new Exception("Post not found");
        }
        post.AddDislike(userId);
        await _postRepository.UpdatePostAsync(post);
    }

    public async Task RemoveDislikeFromPostAsync(PostId postId, UserId userId)
    {
        var post = await _postRepository.GetPostByIdAsync(postId);
        if (post == null)
        {
            throw new Exception("Post not found");
        }
        post.RemoveDislike(userId);
        await _postRepository.UpdatePostAsync(post);
    }

    public async Task AddLikeOnCommentAsync(PostId postId, CommentId commentId, UserId userId)
    {
        var post = await _postRepository.GetPostByIdAsync(postId);
        if (post == null)
        {
            throw new Exception("Post not found");
        }
        post.LikeComment(commentId, userId);
        await _postRepository.UpdatePostAsync(post);
    }

    public async Task RemoveLikeFromCommentAsync(PostId postId, CommentId commentId, UserId userId)
    {
        var post = await _postRepository.GetPostByIdAsync(postId);
        if (post == null)
        {
            throw new Exception("Post not found");
        }
        post.RemoveLikeFromComment(commentId, userId);
        await _postRepository.UpdatePostAsync(post);
    }

    public async Task AddDislikeOnCommentAsync(PostId postId, CommentId commentId, UserId userId)
    {
        var post = await _postRepository.GetPostByIdAsync(postId);
        if (post == null)
        {
            throw new Exception("Post not found");
        }
        post.DislikeComment(commentId, userId);
        await _postRepository.UpdatePostAsync(post);
    }

    public async Task RemoveDislikeFromCommentAsync(PostId postId, CommentId commentId, UserId userId)
    {
        var post = await _postRepository.GetPostByIdAsync(postId);
        if (post == null)
        {
            throw new Exception("Post not found");
        }
        post.RemoveDislikeFromComment(commentId, userId);
        await _postRepository.UpdatePostAsync(post);
    }
}