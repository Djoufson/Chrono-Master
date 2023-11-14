using Domain.ValueObjects.Base;

namespace Domain.ValueObjects;

public sealed record DepartmentId(Guid Id) : ValueObject(Id.ToString());
