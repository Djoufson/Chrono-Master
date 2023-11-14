using Domain.Utilities;
using Domain.ValueObjects;

namespace Domain.Entities;

public class Admin : User
{
    private Admin(
        UserId id,
        Name name,
        Password password,
        string role) : base(id, name, password, role)
    {
    }

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
