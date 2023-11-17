using System.Text;
using Application.Services;
using Domain.Utilities;
using Infrastructure.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(
        this IServiceCollection services,
        IConfiguration cfg)
    {
        var jwtSettings = cfg.GetRequiredSection(JwtSettings.SectionName).Get<JwtSettings>()!;
        services.AddSingleton(jwtSettings);
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters()
                {
                    ClockSkew = TimeSpan.Zero,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
                };
            });

        services.AddAuthorization(opt =>
        {
            opt.AddPolicy(Policies.AdminOnly, p => p.RequireRole(Roles.Admin));
            opt.AddPolicy(Policies.TeacherOnly, p => p.RequireRole(Roles.Teacher));
            opt.AddPolicy(Policies.AcademicManagerOnly, p => p.RequireRole(Roles.AcademicManager));
            opt.AddPolicy(Policies.AdminAndAcademicManagerOnly, p => p.RequireRole(Roles.Admin, Roles.AcademicManager));
        });

        return services;
    }
}
