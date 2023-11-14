using Domain.Entities.Base;
using Domain.ValueObjects;

namespace Domain.Entities;

public class Course : Entity<CourseId>
{
    public Course(CourseId id) : base(id)
    {
    }
}
