using Domain.ValueObjects.Base;

namespace Domain.ValueObjects;

public record Name : ValueObject
{
    public string FirstName { get; private init; }
    public string? LastName { get; private init; }
    private Name(
        string value,
        string firstName,
        string? lastName) : base(value)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public static Name Create(string firstName, string? lastName)
    {
        return new(
            $"{firstName} {lastName}",
            firstName,
            lastName);
    }
}
