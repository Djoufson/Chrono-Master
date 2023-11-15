using System.Collections.ObjectModel;
using Domain.Entities.Base;
using Domain.Utilities;
using Domain.ValueObjects;

namespace Domain.Entities;

public sealed class Teacher : User
{
    public ICollection<Course> Courses { get; private set; } = new Collection<Course>();
    public Teacher(UserId id, Name name, Password password, string role) : base(id, name, password, role)
    {
    }

    private Teacher()
    {
    }

    public static User CreateUnique(Name name, Password password)
    {
        var id = UserId.CreateUnique();
        return new Teacher(id, name, password, Roles.Teacher);
    }
}
