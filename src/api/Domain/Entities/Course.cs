using Domain.Entities.Base;
using Domain.ValueObjects;

namespace Domain.Entities;

public class Course : Entity<CourseId>
{
    public Planning? Planning { get; private set; }
    public Teacher? Teacher { get; private set; }
    public Department Department { get; private set; }
    public string Title { get; private set; }
    public int TotalHours { get; private set; }

    private Course(
        CourseId id,
        string title,
        int totalHours,
        Department department,
        Teacher? teacher,
        Planning? planning) : base(id)
    {
        Title = title;
        TotalHours = totalHours;
        Department = department;
        Teacher = teacher;
        Planning = planning;
    }

    public static Course CreateUnique(
        string title,
        int totalHours,
        Department department,
        Teacher? teacher,
        Planning? planning)
    {
        return new(
            new CourseId(Guid.NewGuid()),
            title,
            totalHours,
            department,
            teacher,
            planning);
    }

    public void AssignTeacher(Teacher teacher)
    {
        ArgumentNullException.ThrowIfNull(teacher);
        Teacher = teacher;
    }

    public void AttachPlanning(Planning planning)
    {
        Planning = planning;
    }
}
