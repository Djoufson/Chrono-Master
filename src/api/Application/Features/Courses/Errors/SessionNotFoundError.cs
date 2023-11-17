using FluentResults;

namespace Application.Features.Courses.Errors;

public class SessionNotFoundError : IError
{
    public List<IError> Reasons => new();

    public string Message => "The session you are requesting for does not or no longer exist";

    public Dictionary<string, object> Metadata => new();
}
