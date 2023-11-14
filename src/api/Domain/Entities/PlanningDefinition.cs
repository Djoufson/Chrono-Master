using System.Collections.ObjectModel;
using Domain.Entities.Base;
using Domain.ValueObjects;

namespace Domain.Entities;

public class PlanningDefinition : Entity<PlanningDefinitionId>
{
    public ICollection<DefinitionItem> Definitions { get; private set; } = new Collection<DefinitionItem>();
    private PlanningDefinition(
        PlanningDefinitionId id) : base(id)
    {
    }

    public static PlanningDefinition CreateUnique()
    {
        return new(
            new PlanningDefinitionId(Guid.NewGuid())
        );
    }

    public void AddDefinition(DefinitionItem definition)
    {
        if(Definitions.Any(d => d.Id == definition.Id))
            return;

        Definitions.Add(definition);
    }

    public void RemoveDefinition(DefinitionItem definition)
    {
        Definitions.Remove(definition);
    }
}
