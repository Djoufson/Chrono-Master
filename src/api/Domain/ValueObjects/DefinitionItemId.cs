using Domain.ValueObjects.Base;

namespace Domain.ValueObjects;

public sealed record DefinitionItemId(Guid Id) : ValueObject(Id.ToString());
