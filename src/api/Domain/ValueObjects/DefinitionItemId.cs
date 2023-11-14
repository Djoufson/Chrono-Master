using Domain.ValueObjects.Base;

namespace Domain.ValueObjects;

public record DefinitionItemId(Guid Id) : ValueObject(Id.ToString());
