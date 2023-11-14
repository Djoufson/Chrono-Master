using Domain.ValueObjects;

namespace Domain.Entities;

public class Admin : User
{
    public Admin(UserId id, Name name, Password password) : base(id, name, password)
    {
    }
}
