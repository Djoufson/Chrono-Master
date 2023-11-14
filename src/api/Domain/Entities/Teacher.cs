using Domain.ValueObjects;

namespace Domain.Entities;

public class Teacher : User
{
    public Teacher(UserId id, Name name, Password password) : base(id, name, password)
    {
    }

    public static User CreateUnique(Name name, Password password)
    {
        var id = UserId.CreateUnique();
        return new Teacher(id, name, password);
    }
}
