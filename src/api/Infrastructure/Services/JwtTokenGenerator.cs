using System.Security.Claims;
using Application.Services;

namespace Infrastructure.Services;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    public string GenerateToken(IEnumerable<Claim> claims)
    {
        return "token";
    }

    public string GenerateToken(params Claim[] claims)
    {
        return "token";
    }
}
