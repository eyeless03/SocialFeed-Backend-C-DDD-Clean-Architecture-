namespace Domains.ValueObjects;

public readonly record struct PostId(Guid Value)
{
    public static PostId New() => new(Guid.NewGuid());
    public static PostId From(Guid id) => id == Guid.Empty ? throw new ArgumentNullException(nameof(id)) : new(id);
    public override string ToString() => Value.ToString();
}