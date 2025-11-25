namespace Domains.ValueObjects;

public readonly record struct CommentId(Guid Value)
{
    public static CommentId New() => new(Guid.NewGuid());
    public static CommentId From(Guid id) => id == Guid.Empty ? throw new ArgumentNullException(nameof(id)) : new(id);
    public override string ToString() => Value.ToString();
}