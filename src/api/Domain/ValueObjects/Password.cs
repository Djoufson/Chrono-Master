using Domain.ValueObjects.Base;

namespace Domain.ValueObjects;

public sealed record Password : ValueObject
{
    private Password(string value) : base(value)
    {
    }

    public static Password Create(string hash)
    {
        return new(hash);
    }
}
