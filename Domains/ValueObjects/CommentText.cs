namespace Domains.ValueObjects;

public readonly record struct CommentText
{
    public string Value { get; }

    public CommentText(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(text));
        }

        if (text.Length > 250)
        {
            throw new ArgumentException("Value cannot be longer than 250 characters.", nameof(text));
        }
        
        Value = text;
    }
    public override string ToString() => Value;
}