using Application.Features.Departments.Errors;
using Application.Persistence;
using Application.Persistence.Base;
using Domain.Entities;
using Domain.ValueObjects;
using FluentResults;
using MediatR;

namespace Application.Features.Departments;

public class CreateDepartment
    : IRequestHandler<CreateDepartment.CreateDepartmentRequest, Result<CreateDepartment.CreateDepartmentResponse>>
{
    private readonly IRepository<Department, DepartmentId> _departmentRepository;
    private readonly IRepository<AcademicManager, UserId> _managerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateDepartment(
        IRepository<Department, DepartmentId> departmentRepository,
        IRepository<AcademicManager, UserId> managerRepository,
        IUnitOfWork unitOfWork)
    {
        _departmentRepository = departmentRepository;
        _managerRepository = managerRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<CreateDepartmentResponse>> Handle(CreateDepartmentRequest request, CancellationToken cancellationToken)
    {
        // Check if there is no Department with the supplied code
        var exists = await _departmentRepository.ExistsAsync(d => d.Code == request.Code, cancellationToken);
        if(exists)
            return Result.Fail(DepartmentErrors.DepartmentConflictError);

        // Create the department
        AcademicManager? manager = null;
        if(request.ManagerId is Guid id)
            manager = await _managerRepository.GetByIdAsync(UserId.Create(id), cancellationToken);

        var department = Department.CreateUnique(
            request.Title,
            request.Code,
            manager
        );

        // Save it to the database
        await _departmentRepository.AddAsync(department, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Return
        return new CreateDepartmentResponse(
            department.Id.Value,
            department.Title,
            department.Code,
            department.Manager?.Id.Value
        );
    }

    public record CreateDepartmentRequest(
        string Title,
        string Code,
        Guid? ManagerId
    ) : IRequest<Result<CreateDepartmentResponse>>;

    public record CreateDepartmentResponse(
        string Id,
        string Title,
        string Code,
        string? ManagerId
    );
}
