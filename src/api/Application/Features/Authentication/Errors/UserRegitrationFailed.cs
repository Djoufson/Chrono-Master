using FluentResults;

namespace Application.Features.Authentication.Errors;

public class UserRegitrationFailed : IError
{
    public List<IError> Reasons => throw new NotImplementedException();

    public string Message { get; }
    public Dictionary<string, object> Metadata => throw new NotImplementedException();

    public UserRegitrationFailed(string message)
    {
        Message = message;
    }
}