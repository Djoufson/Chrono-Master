using Domain.Entities.Base;
using Domain.ValueObjects;

namespace Domain.Entities;

public class Planning : Entity<PlanningId>
{
    private Planning(PlanningId id) : base(id)
    {
    }
}
