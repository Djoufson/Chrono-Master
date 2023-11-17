using Application.Persistence;
using FluentResults;
using MediatR;

namespace Application.Features.Courses;

public class GetAllCourses
    : IRequestHandler<GetAllCourses.GetAllCoursesRequest, Result<GetAllCourses.GetAllCoursesResponse>> 
{
    private readonly ICoursesRepository _coursesRepository;

    public GetAllCourses(ICoursesRepository coursesRepository)
    {
        _coursesRepository = coursesRepository;
    }

    public async Task<Result<GetAllCoursesResponse>> Handle(GetAllCoursesRequest request, CancellationToken cancellationToken)
    {
        // Retrieve all courses
        var courses = await _coursesRepository.GetAllAsync(cancellationToken);
        return new GetAllCoursesResponse(
            courses
                .Select(c => new Course(
                    c.Id.Value,
                    c.Title,
                    c.TotalHours,
                    c.Teacher?.Name.Value,
                    c.Department.Code))
                .ToArray()
        );
    }

    public record GetAllCoursesRequest() : IRequest<Result<GetAllCoursesResponse>>;
    public record GetAllCoursesResponse(
        Course[] Courses
    );

    public record Course(
        string Id,
        string Title,
        int TotalHours,
        string? Teacher,
        string Department
    );
}
