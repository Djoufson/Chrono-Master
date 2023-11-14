using Domain.Entities.Base;
using Domain.Utilities;
using Domain.ValueObjects;

namespace Domain.Entities;

public sealed class Admin : User
{
    private Admin(
        UserId id,
        Name name,
        Password password,
        string role) : base(id, name, password, role)
    {
    }

    #pragma warning disable CS8618
    private Admin()
    {
    }
    #pragma warning restore

    public static User CreateUnique(
        Name name,
        Password password)
    {
        return new Admin(
            UserId.CreateUnique(),
            name,
            password,
            Roles.Admin
        );
    }
}
