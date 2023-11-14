using FluentResults;

namespace Application.Features.Courses.Errors;

public class DepartmentNotFoundError : IError
{
    public List<IError> Reasons => new();

    public string Message => "The department you supplied does not exist";

    public Dictionary<string, object> Metadata => new();
}
