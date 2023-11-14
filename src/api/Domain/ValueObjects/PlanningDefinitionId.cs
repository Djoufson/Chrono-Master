using Domain.ValueObjects.Base;

namespace Domain.ValueObjects;

public record DefinitionId(Guid Id) : ValueObject(Id.ToString());
