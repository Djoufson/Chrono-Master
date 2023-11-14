using Domain.Entities.Base;
using Domain.ValueObjects;

namespace Domain.Entities;

public abstract class User : Entity<UserId>
{
    public Name Name { get; set; }
    public Password Password { get; private set; }
    protected User(UserId id, Name name, Password password) : base(id)
    {
        Password = password;
        Name = name;
    }
}
