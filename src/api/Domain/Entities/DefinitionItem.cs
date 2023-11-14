using Domain.Entities.Base;
using Domain.ValueObjects;

namespace Domain.Entities;

public class DefinitionItem : Entity<DefinitionItemId>
{
    private DefinitionItem(DefinitionItemId id) : base(id)
    {
    }
}
