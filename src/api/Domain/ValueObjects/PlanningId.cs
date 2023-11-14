using Domain.ValueObjects.Base;

namespace Domain.ValueObjects;

public record PlanningId(Guid Id) : ValueObject(Id.ToString());
