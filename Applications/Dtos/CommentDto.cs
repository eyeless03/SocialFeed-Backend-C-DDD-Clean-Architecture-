namespace Applications.Dtos;

public record CommentDto(
    Guid Id,
    string Content,
    long Likes,
    long Dislikes,
    Guid AuthorId
);