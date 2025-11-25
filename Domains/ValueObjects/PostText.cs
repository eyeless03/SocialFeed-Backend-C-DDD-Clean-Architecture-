namespace Domains.ValueObjects;

public readonly record struct PostText
{
    public string Value { get; }
    public PostText(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentNullException(nameof(value));
        }

        if (value.Length > 250)
        {
            throw new ArgumentOutOfRangeException(nameof(value));
        }
        Value = value;
    }

    public override string ToString() => Value;
}