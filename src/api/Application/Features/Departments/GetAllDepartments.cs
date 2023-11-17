using Application.Persistence.Base;
using Domain.ValueObjects;
using FluentResults;
using MediatR;
using DomainDepartment = Domain.Entities.Department;

namespace Application.Features.Departments;

public class GetAllDepartments
    : IRequestHandler<GetAllDepartments.GetAllDepartmentsRequest, Result<GetAllDepartments.GetAllDepartmentsResponse>>
{
    private readonly IRepository<DomainDepartment, DepartmentId> _departmentRepository;

    public GetAllDepartments(IRepository<DomainDepartment, DepartmentId> departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public async Task<Result<GetAllDepartmentsResponse>> Handle(GetAllDepartmentsRequest request, CancellationToken cancellationToken)
    {
        // Retrieve all departments
        var departments = await _departmentRepository.GetAllAsync(cancellationToken);
        return new GetAllDepartmentsResponse(
            departments
                .Select(d => new Department(
                    d.Id.Value,
                    d.Title,
                    d.Code,
                    d.Manager?.Id.Value))
                .ToArray()
        );
    }

    public record GetAllDepartmentsRequest() : IRequest<Result<GetAllDepartmentsResponse>>;
    public record GetAllDepartmentsResponse(
        Department[] Departments
    ) : IRequest<Result<Department>>;
}
