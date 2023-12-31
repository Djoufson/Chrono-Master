using System.Security.Claims;
using Application.Features.Authentication.Errors;
using Application.Persistence.Base;
using Application.Services;
using Domain.Entities.Base;
using Domain.ValueObjects;
using FluentResults;
using MediatR;

namespace Application.Features.Authentication;

public class Login : IRequestHandler<Login.LoginRequest, Result<Login.LoginResponse>>
{
    private readonly IRepository<User, UserId> _userRepository;
    private readonly IJwtTokenGenerator _tokenGenerator;

    public Login(
        IRepository<User, UserId> userRepository,
        IJwtTokenGenerator tokenGenerator)
    {
        _userRepository = userRepository;
        _tokenGenerator = tokenGenerator;
    }

    public async Task<Result<LoginResponse>> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        // Get the user from the database
        var userId = UserId.Create(request.Identifier);
        var user = await _userRepository.GetByIdAsync(userId, cancellationToken);

        // Check if the password matches
        if(user is null)
            return Result.Fail(AuthErrors.UserNotFoundError);

        if(user.Password != Password.Create(request.Password))
            return Result.Fail(AuthErrors.MismatchPasswordError);

        // Generate the token
        var token = _tokenGenerator.GenerateToken(
            new Claim(ClaimTypes.Role, user.Role),
            new Claim(ClaimTypes.NameIdentifier, user.Id.Value),
            new Claim(ClaimTypes.Name, user.Name.Value)
        );

        // Return
        return new LoginResponse(token);
    }

    public record LoginRequest(
        Guid Identifier,
        string Password
    ) : IRequest<Result<LoginResponse>>;

    public record LoginResponse(
        string Token
    );
}
