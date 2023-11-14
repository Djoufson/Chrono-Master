using Domain.Entities.Base;
using Domain.ValueObjects;

namespace Domain.Entities;

public class Department : Entity<DepartmentId>
{
    private Department(DepartmentId id) : base(id)
    {
    }
}
