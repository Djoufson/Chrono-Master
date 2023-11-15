using FluentResults;

namespace Application.Features.Courses.Errors;

public class CourseNotFoundError : IError
{
    public List<IError> Reasons => new();

    public string Message => "The requested course does not exist";

    public Dictionary<string, object> Metadata => new();
}
