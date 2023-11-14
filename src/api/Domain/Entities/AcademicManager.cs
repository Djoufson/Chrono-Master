using Domain.ValueObjects;

namespace Domain.Entities;

public class AcademicManager : User
{
    public AcademicManager(UserId id, Name name, Password password) : base(id, name, password)
    {
    }
}
