using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Services;
using Infrastructure.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services;

internal class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly JwtSettings _jwt;

    public JwtTokenGenerator(IConfiguration configuration)
    {
        _jwt = configuration.GetSection(JwtSettings.SectionName).Get<JwtSettings>()!;
    }
    public string GenerateToken(IEnumerable<Claim> claims)
    {
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.SecretKey)),
            SecurityAlgorithms.HmacSha256);

        var securityToken = new JwtSecurityToken(
            claims: claims,
            issuer: _jwt.Issuer,
            audience: _jwt.Audience,
            expires: DateTime.Now.AddDays(_jwt.ExpirationInDays),
            signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }

    public string GenerateToken(params Claim[] claims)
    {
        return GenerateToken((IEnumerable<Claim>)claims);
    }
}
