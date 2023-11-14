using Domain.ValueObjects.Base;

namespace Domain.ValueObjects;

public record PlanningDefinitionId(Guid Id) : ValueObject(Id.ToString());
