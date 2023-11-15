using Domain.ValueObjects;

namespace Domain.Entities.Base;

public abstract class User : Entity<UserId>
{
    public Name Name { get; private set; }
    public Password Password { get; private set; }
    public string Role { get; private set; }
    protected User(UserId id, Name name, Password password, string role) : base(id)
    {
        Password = password;
        Name = name;
        Role = role;
    }

    #pragma warning disable CS8618
    protected User()
    {
    }
    #pragma warning restore
}
