using Application.Features.Courses.Errors;
using Application.Persistence;
using Application.Persistence.Base;
using Domain.Entities;
using Domain.ValueObjects;
using FluentResults;
using MediatR;

namespace Application.Features.Courses;

public class CreateCourse : IRequestHandler<CreateCourse.CreateCourseRequest, Result<CreateCourse.CreateCourseResponse>>
{
    private readonly IRepository<Department, DepartmentId> _departmentsRepository;
    private readonly IRepository<Course, CourseId> _coursesRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCourse(
        IRepository<Department, DepartmentId> departmentsRepository,
        IRepository<Course, CourseId> coursesRepository,
        IUnitOfWork unitOfWork)
    {
        _departmentsRepository = departmentsRepository;
        _coursesRepository = coursesRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<CreateCourseResponse>> Handle(CreateCourseRequest request, CancellationToken cancellationToken)
    {
        // Create the course
        var departmentId = new DepartmentId(request.DepartmentId);
        var department = await _departmentsRepository.GetByIdAsync(departmentId, cancellationToken);
        if(department is null)
            return Result.Fail(CourseErrors.DepartmentNotFoundError);

        var course = Course.CreateUnique(
            request.Title,
            request.TotalHours,
            department,
            null,
            null);

        // Save to the database
        course = await _coursesRepository.AddAsync(course, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Return
        return new CreateCourseResponse(
            course.Id.Value,
            course.Title,
            course.TotalHours,
            course.Department.Id.Value
        );
    }

    public record CreateCourseRequest(
        string Title,
        int TotalHours,
        Guid DepartmentId
    ) : IRequest<Result<CreateCourseResponse>>;

    public record CreateCourseResponse(
        string Id,
        string Title,
        int TotalHours,
        string DepartmentId
    );
}
