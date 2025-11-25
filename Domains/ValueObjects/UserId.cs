namespace Domains.ValueObjects;

public readonly record struct UserId(Guid Value)
{
    public static UserId New()=> new (Guid.NewGuid());
    public static UserId From(Guid id) => id == Guid.Empty ? throw new ArgumentNullException(nameof(id)) : new(id);
    public override string ToString() => Value.ToString();
}