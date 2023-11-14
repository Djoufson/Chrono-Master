using Domain.ValueObjects.Base;

namespace Domain.ValueObjects;

public sealed record DefinitionId(Guid Id) : ValueObject(Id.ToString());
