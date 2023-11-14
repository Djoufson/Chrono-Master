using Domain.ValueObjects.Base;

namespace Domain.ValueObjects;

public sealed record PlanningId(Guid Id) : ValueObject(Id.ToString());
