using Domains.ValueObjects;

namespace Domains.Entities;

public class Post
{
    private readonly HashSet<UserId> _likedUsers = new();
    private readonly HashSet<UserId> _dislikedUsers = new();
    public IReadOnlyCollection<UserId> LikedUsers => _likedUsers;
    public IReadOnlyCollection<UserId> DislikedUsers => _dislikedUsers;
    public long LikesCount => LikedUsers.Count;
    public long DislikesCount => DislikedUsers.Count;
    private PostId _id;
    private PostText _title;
    public PostText Title => _title;
    public PostId Id => _id;
    private UserId _authorId;
    public UserId AuthorId => _authorId;
    private readonly List<Comment> _comments = new();
    public IReadOnlyCollection<Comment> Comments => _comments;
    public void AddLike(UserId userId)
    {
        if (_likedUsers.Contains(userId))
        {
            return;
        }
        _dislikedUsers.Remove(userId);
        _likedUsers.Add(userId);
    }

    public void LikeComment(CommentId commentId, UserId userId)
    {
        var comment = _comments.FirstOrDefault(c => c.Id == commentId);
        if (comment == null)return;
        comment.AddLike(userId);
    }

    public void DislikeComment(CommentId commentId, UserId userId)
    {
        var comment = _comments.FirstOrDefault(c => c.Id == commentId);
        if (comment == null)return;
        comment.AddDislike(userId);
    }

    public void RemoveLikeFromComment(CommentId commentId,UserId userId)
    {
        var comment = _comments.FirstOrDefault(c => c.Id == commentId);
        if (comment == null)return;
        comment.RemoveLike(userId);
    }

    public void RemoveDislikeFromComment(CommentId commentId, UserId userId)
    {
        var comment = _comments.FirstOrDefault(c => c.Id == commentId);
        if (comment == null)return;
        comment.RemoveDislike(userId);
    }
    
    public void RemoveComment(CommentId commentId, UserId authorId)
    {
        var comment =  _comments.FirstOrDefault(c => c.Id == commentId);
        if (comment == null)
        {
            return;
        }

        if (authorId != comment.AuthorId)
        {
            return;
        }
        _comments.Remove(comment);
    }

    public void RemoveLike(UserId userId)
    {
        _likedUsers.Remove(userId);
    }

    public void AddDislike(UserId userId)
    {
        if (_dislikedUsers.Contains(userId))
        {
            return;
        }
        _likedUsers.Remove(userId);
        _dislikedUsers.Add(userId);
    }

    public void RemoveDislike(UserId userId)
    {
        _dislikedUsers.Remove(userId);
    }

    public Comment AddComment(CommentText text, UserId authorId)
    {
        var comment = new Comment(text, authorId);
        _comments.Add(comment);
        return comment;
    }

    public Post(PostText title, UserId authorId)
    {
        _authorId = authorId;
        _title = title;
        _id = PostId.New();
    }
}