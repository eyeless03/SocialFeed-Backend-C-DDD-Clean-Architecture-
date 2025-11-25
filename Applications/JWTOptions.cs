namespace Applications;

public class JWTOptions
{
    public string Issuer { get; set; } = null!;
    public string? Audience { get; set; }
    public string? SecretKey { get; set; }
    public int ExpiresIn { get; set; }
}