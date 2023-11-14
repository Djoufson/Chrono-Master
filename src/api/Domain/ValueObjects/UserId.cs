using Domain.ValueObjects.Base;

namespace Domain.ValueObjects;

public sealed record UserId : ValueObject
{
    private UserId(string value) : base(value)
    {
    }

    public static UserId CreateUnique()
    {
        return new(Guid.NewGuid().ToString());
    }

    public static UserId Create(Guid id)
    {
        return new(id.ToString());
    }
}
