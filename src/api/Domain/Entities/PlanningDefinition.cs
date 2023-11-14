using Domain.Entities.Base;
using Domain.ValueObjects;

namespace Domain.Entities;

public class PlanningDefinition : Entity<PlanningDefinitionId>
{
    public PlanningDefinition(PlanningDefinitionId id) : base(id)
    {
    }
}
