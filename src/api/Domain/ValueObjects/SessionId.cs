using Domain.ValueObjects.Base;

namespace Domain.ValueObjects;

public record SessionId(Guid Id) : ValueObject(Id.ToString());
