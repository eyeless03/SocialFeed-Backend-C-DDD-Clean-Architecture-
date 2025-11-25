using Domains.ValueObjects;
public interface IPostService
{
    Task<PostId> CreatePostAsync(PostText text, UserId authorId);
    Task AddLikeOnPostAsync(PostId postId, UserId userId);
    Task RemoveLikeFromPostAsync(PostId postId, UserId userId);
    Task AddDislikeOnPostAsync(PostId postId, UserId userId);
    Task RemoveDislikeFromPostAsync(PostId postId, UserId userId);
    Task<CommentId> AddCommentAsync(PostId postId, UserId userId, CommentText text);
    Task AddLikeOnCommentAsync(PostId postId, CommentId commentId, UserId userId);
    Task RemoveLikeFromCommentAsync(PostId postId, CommentId commentId, UserId userId);
    Task AddDislikeOnCommentAsync(PostId postId, CommentId commentId, UserId userId);
    Task RemoveDislikeFromCommentAsync(PostId postId, CommentId commentId, UserId userId);
}