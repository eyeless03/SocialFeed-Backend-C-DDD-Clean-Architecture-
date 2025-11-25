using Domains.ValueObjects;

namespace Domains.Entities;

public class Comment
{
    private readonly HashSet<UserId> _likedUsers = new();
    private readonly HashSet<UserId> _dislikedUsers = new();
    public IReadOnlyCollection<UserId> LikedUsers => _likedUsers;
    public IReadOnlyCollection<UserId> DislikedUsers => _dislikedUsers;
    public long LikesCount => LikedUsers.Count;
    public long DislikesCount => DislikedUsers.Count;
    private UserId _authorId;
    public UserId AuthorId => _authorId;
    private CommentId _id;
    public CommentId Id => _id;
    private CommentText _text;
    public CommentText Text => _text;
    
    public void AddLike(UserId userId)
    {
        if (_likedUsers.Contains(userId))
        {
            return;
        }
        _dislikedUsers.Remove(userId);
        _likedUsers.Add(userId);
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
    public Comment(CommentText text, UserId authorId)
    {
        _authorId = authorId;
        _text = text;
        _id = CommentId.New();
    }
}