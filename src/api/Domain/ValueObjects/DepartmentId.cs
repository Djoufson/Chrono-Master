using Domain.ValueObjects.Base;

namespace Domain.ValueObjects;

public record DepartmentId(Guid Id) : ValueObject(Id.ToString());
