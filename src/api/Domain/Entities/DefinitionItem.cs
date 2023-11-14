using Domain.Entities.Base;
using Domain.ValueObjects;

namespace Domain.Entities;

public class DefinitionItem : Entity<DefinitionItemId>
{
    public DayOfWeek DayOfWeek { get; private set; }
    public TimeOnly StartTime { get; private set; }
    public TimeSpan Duration { get; private set; }
    public PlanningDefinition Definition { get; set; }

    private DefinitionItem(
        DefinitionItemId id,
        PlanningDefinition definition,
        DayOfWeek dayOfWeek,
        TimeOnly startTime,
        TimeSpan duration) : base(id)
    {
        DayOfWeek = dayOfWeek;
        StartTime = startTime;
        Duration = duration;
        Definition = definition;
    }

    public static DefinitionItem CreateUnique(
        PlanningDefinition definition,
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
