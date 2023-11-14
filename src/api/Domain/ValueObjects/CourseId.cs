using Domain.ValueObjects.Base;

namespace Domain.ValueObjects;

public sealed record CourseId(Guid Id) : ValueObject(Id.ToString());
