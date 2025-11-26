using System.IdentityModel.Tokens.Jwt;
using Xunit;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Applications;
using Applications.Dtos;
using Applications.Services;
using Domains.ValueObjects;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebApplication3;
using WebApplication3.Infrastructure;

public class JwtServiceTests
{
    private readonly JWTService _jwtService;
    private readonly JWTOptions _jwtOptions;

    public JwtServiceTests()
    {
        _jwtOptions = new JWTOptions
        {
            Issuer = "myapplication",
            Audience = "myapplication",
            SecretKey = "123456789101112131415161718192021222324252627282930",
            ExpiresMinutes = 60
        };
        var optionsWrapper = new OptionsWrapper<JWTOptions>(_jwtOptions);
        _jwtService = new JWTService(optionsWrapper);
    }

        [Fact]
        public void GenerateJWTToken_Returns_NonEmpty_String()
        {
            var user = new UserDto(Guid.NewGuid(), new Username("test"));
            var token = _jwtService.GenerateJWTToken(user);
            Assert.False(string.IsNullOrEmpty(token));
        }
        [Fact]
        public void GenerateJWTToken_Contains_Correct_Claims()
        {
            var user = new UserDto(Guid.NewGuid(), new Username("test"));
            var token = _jwtService.GenerateJWTToken(user);
            var handler = new JwtSecurityTokenHandler();
            var claims = handler.ReadJwtToken(token).Claims.ToList();
            var userId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var username = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            Assert.Equal(user.Id.ToString(), userId);
            Assert.Equal(user.Username.Value, username);
            Assert.Equal(_jwtOptions.Issuer, claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Iss)?.Value);
            Assert.Contains(_jwtOptions.Audience, claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Aud)?.Value);
        }
}
