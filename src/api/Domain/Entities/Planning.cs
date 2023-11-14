using Domain.Entities.Base;
using Domain.ValueObjects;

namespace Domain.Entities;

public sealed class Planning : Entity<PlanningId>
{
    public Definition Definition { get; set; }
    public IReadOnlyList<Session> Sessions { get; private set; } = Array.Empty<Session>();
    private Planning(
        PlanningId id,
        Definition definition) : base(id)
    {
        Definition = definition;
    }

    public static Planning CreateUnique(
        Definition definition)
    {
        return new(
            new PlanningId(Guid.NewGuid()),
            definition
        );
    }

    public void GenerateSessions()
    {
        Sessions = Array.Empty<Session>();
    }
}
