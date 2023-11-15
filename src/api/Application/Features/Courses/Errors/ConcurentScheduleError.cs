using FluentResults;

namespace Application.Features.Courses.Errors;

public class ConcurentScheduleError : IError
{
    public List<IError> Reasons => new();

    public string Message => "This course has already a session scheduled at this day and time";

    public Dictionary<string, object> Metadata => new();
}
