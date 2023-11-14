using Domain.ValueObjects.Base;

namespace Domain.ValueObjects;

public sealed record SessionId(Guid Id) : ValueObject(Id.ToString());
