namespace Application.Features.Departments;

public record Department(
    string Id,
    string Title,
    string Code,
    string? ManagerId
);
