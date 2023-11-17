using FluentResults;

namespace Application.Features.Courses.Errors;

public class PlanningNotSetError : IError
{
    public List<IError> Reasons => new();

    public string Message => "No planning set for this course.";

    public Dictionary<string, object> Metadata => new();
}
