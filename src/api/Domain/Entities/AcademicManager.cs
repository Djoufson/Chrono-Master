using Domain.Entities.Base;
using Domain.Utilities;
using Domain.ValueObjects;

namespace Domain.Entities;

public sealed class AcademicManager : User
{
    public Department Department { get; private set; }
    private AcademicManager(
        UserId id,
        Name name,
        Password password,
        Department department,
        string role) : base(id, name, password, role)
    {
        Department = department;
    }

    public static User CreateUnique(
        Name name,
        Password password,
        Department department)
    {
        var id = UserId.CreateUnique();
        return new AcademicManager(
            id,
            name,
            password,
            department,
            Roles.AcademicManager);
    }
}
