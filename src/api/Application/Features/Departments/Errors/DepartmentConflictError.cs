using FluentResults;

namespace Application.Features.Departments.Errors;

public class DepartmentConflictError : IError
{
    public List<IError> Reasons => new();

    public string Message => "A department with this specific code already exists";

    public Dictionary<string, object> Metadata => new();
}
