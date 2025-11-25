namespace Domains.ValueObjects;

public readonly record struct Username
{
    public string Value { get; }
    private readonly static char[] InvalidChars = { ';', '!', '?', '.', '"' };
    public Username(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(value));
        }

        if (value.Any(c => InvalidChars.Contains(c)))
        {
            throw new ArgumentException("Invalid username character.", nameof(value));
        }

        if (value.Length > 25)
        {
            throw new ArgumentException("Username is too long.", nameof(value));
        }
        Value = value;
    }
    public override string ToString() => Value;
}