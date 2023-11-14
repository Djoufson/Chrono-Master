using System.Security.Claims;

namespace Application.Services;

public interface IJwtTokenGenerator
{
    string GenerateToken(IEnumerable<Claim> claims);
    string GenerateToken(params Claim[] claims);
}
