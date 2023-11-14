using Domain.Entities.Base;
using Domain.ValueObjects;

namespace Domain.Entities;

public class Course : Entity<CourseId>
{
    public Department Department { get; private set; }
    public string Title { get; set; }
    public int TotalHours { get; set; }

    private Course(
        CourseId id,
        string title,
        int totalHours,
        Department department) : base(id)
    {
        Title = title;
        TotalHours = totalHours;
        Department = department;
    }

    public static Course CreateUnique(
        string title,
        int totalHours,
        Department department)
    {
        return new(
            new CourseId(Guid.NewGuid()),
            title,
            totalHours,
            department);
    }
}
