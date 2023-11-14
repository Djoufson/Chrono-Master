using Domain.ValueObjects.Base;

namespace Domain.ValueObjects;

public record CourseId(Guid Id) : ValueObject(Id.ToString());
