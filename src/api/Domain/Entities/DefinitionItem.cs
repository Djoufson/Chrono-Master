using Domain.Entities.Base;
using Domain.ValueObjects;

namespace Domain.Entities;

public sealed class DefinitionItem : Entity<DefinitionItemId>, IComparable<DefinitionItem>
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

    public int CompareTo(DefinitionItem? other)
    {
        if (other == null)
            return 1;

        // Compare DayOfWeek
        int dayComparison = DayOfWeek.CompareTo(other.DayOfWeek);
        if (dayComparison != 0)
            return dayComparison;

        // If DayOfWeek is the same, compare StartTime
        int timeComparison = StartTime.CompareTo(other.StartTime);
        if (timeComparison != 0)
            return timeComparison;

        // If DayOfWeek and StartTime are the same, compare Duration
        return Duration.CompareTo(other.Duration);
    }
}
