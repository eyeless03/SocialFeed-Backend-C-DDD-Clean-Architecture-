namespace Applications.Dtos;

public record PostDto(
    Guid Id,
    string Title,
    Guid AuthorId,
    long Likes,
    long Dislikes,
    IReadOnlyCollection<CommentDto> Comments 
    );