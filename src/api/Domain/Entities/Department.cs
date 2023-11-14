using Domain.Entities.Base;
using Domain.ValueObjects;

namespace Domain.Entities;

public class Department : Entity<DepartmentId>
{
    public string Title { get; private set; }
    public string Code { get; private set; }
    public AcademicManager? Manager { get; private set; }
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
