namespace Domains.ValueObjects;

public readonly record struct PasswordHash
{
    public string Value { get; }
    public static PasswordHash FromHashed(string hash) => new PasswordHash(hash);

    public PasswordHash(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentNullException(nameof(value));
        }
        Value  = value;
    }
}