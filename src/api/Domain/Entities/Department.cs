using System.Collections.ObjectModel;
using Domain.Entities.Base;
using Domain.ValueObjects;

namespace Domain.Entities;

public sealed class Department : Entity<DepartmentId>
{
    public string Title { get; private set; }
    public string Code { get; private set; }
    public AcademicManager? Manager { get; private set; }
    public ICollection<Course> Courses { get; private set; } = new Collection<Course>();

    private Department(
        DepartmentId id,
        string title,
        string code,
        AcademicManager? manager) : base(id)
    {
        Code = code;
        Title = title;
        Manager = manager;
    }

    #pragma warning disable CS8618
    private Department()
    {
    }
    #pragma warning restore

    public static Department CreateUnique(
        string title,
        string code,
        AcademicManager? manager
    )
    {
        return new Department(
            new DepartmentId(Guid.NewGuid()),
            title,
            code,
            manager
        );
    }

    public void AssignManager(AcademicManager manager)
    {
        ArgumentNullException.ThrowIfNull(manager);
        Manager = manager;
    }
}
