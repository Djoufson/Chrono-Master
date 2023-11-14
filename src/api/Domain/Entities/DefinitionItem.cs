using Domain.Entities.Base;
using Domain.ValueObjects;

namespace Domain.Entities;

public sealed class DefinitionItem : Entity<DefinitionItemId>
{
    public DayOfWeek DayOfWeek { get; private set; }
    public TimeOnly StartTime { get; private set; }
    public TimeSpan Duration { get; private set; }
    public Definition Definition { get; private set; }

    private DefinitionItem(
        DefinitionItemId id,
        Definition definition,
        DayOfWeek dayOfWeek,
        TimeOnly startTime,
        TimeSpan duration) : base(id)
    {
        DayOfWeek = dayOfWeek;
        StartTime = startTime;
        Duration = duration;
        Definition = definition;
    }

    #pragma warning disable CS8618
    private DefinitionItem()
    {
    }
    #pragma warning restore

    public static DefinitionItem CreateUnique(
        Definition definition,
        DayOfWeek dayOfWeek,
        TimeOnly startTime,
        TimeSpan duration)
    {
        return new(
            new DefinitionItemId(Guid.NewGuid()),
            definition,
            dayOfWeek,
            startTime,
            duration
        );
    }

    public void UpdateDefinitionItem(
        DayOfWeek dayOfWeek,
        TimeOnly startTime,
        TimeSpan duration)
    {
        DayOfWeek = dayOfWeek;
        StartTime = startTime;
        Duration = duration;
    }
}
