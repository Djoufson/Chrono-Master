using Application.Features.Authentication.Errors;
using Application.Persistence;
using Application.Persistence.Base;
using Domain.Entities;
using Domain.Entities.Base;
using Domain.ValueObjects;
using FluentResults;
using MediatR;

namespace Application.Features.Authentication;

public class Register : IRequestHandler<Register.RegisterRequest, Result<Register.RegisterResponse>>
{
    private readonly IRepository<Department, DepartmentId> _departmentRepository;
    private readonly IRepository<User, UserId> _usersRepository;
    private readonly IUnitOfWork _unitOfWork;

    public Register(
        IRepository<Department, DepartmentId> departmentRepository,
        IRepository<User, UserId> usersRepository,
        IUnitOfWork unitOfWork)
    {
        _departmentRepository = departmentRepository;
        _usersRepository = usersRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<RegisterResponse>> Handle(RegisterRequest request, CancellationToken cancellationToken)
    {
        // Create the entity based on the supplied UserType
        var name = Name.Create(request.FirstName, request.LastName);
        var password = Password.Create(request.Password);

        var user = request.UserType switch
        {
            UserType.Admin => Admin.CreateUnique(name, password),
            UserType.AcademicManager => await CreateAcademicManager(name, password, request.DepartmentId),
            _ => Teacher.CreateUnique(name, password)
        };

        // Save to the database
        if(user is null)
            return Result.Fail(AuthErrors.UserRegitrationFailed("The Department field is mendatory in order to create an Academic Manager"));

        await _usersRepository.AddAsync(user, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Return the created user
        return new RegisterResponse(
            user.Id.Value,
            user.Name.Value,
            user.Role
        );
    }

    private async Task<User?> CreateAcademicManager(Name name, Password password, string? departmentId)
    {
        if (departmentId is null)
            return null;

        var id = new DepartmentId(Guid.Parse(departmentId));
        var department = await _departmentRepository.GetByIdAsync(id) ?? throw new Exception("");
        return AcademicManager.CreateUnique(name, password, department);
    }

    public record RegisterRequest(
        UserType UserType,
        string FirstName,
        string? LastName,
        string Password,
        string? DepartmentId
    ) : IRequest<Result<RegisterResponse>>;

    public record RegisterResponse(
        string Identifier,
        string FullName,
        string Role);
}

public enum UserType
{
    Admin,
    AcademicManager,
    Teacher
}
