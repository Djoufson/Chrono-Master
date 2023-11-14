using System.Collections.ObjectModel;
using Domain.Entities.Base;
using Domain.ValueObjects;

namespace Domain.Entities;

public sealed class Definition : Entity<DefinitionId>
{
    public ICollection<DefinitionItem> Items { get; private set; } = new Collection<DefinitionItem>();
    private Definition(
        DefinitionId id) : base(id)
    {
    }

    #pragma warning disable CS8618
    private Definition()
    {
    }
    #pragma warning restore

    public static Definition CreateUnique()
    {
        return new(
            new DefinitionId(Guid.NewGuid())
        );
    }
}
