using Domain.Entities.Base;
using Domain.ValueObjects;

namespace Domain.Entities;

public sealed class Session : Entity<SessionId>
{
    public Planning Planning { get; private set; }
    public DateTime StartDateTime { get; private set; }
    public TimeSpan Duration { get; private set; }

    private Session(
        SessionId id,
        Planning planning,
        DateTime startDateTime,
        TimeSpan duration) : base(id)
    {
        StartDateTime = startDateTime;
        Duration = duration;
        Planning = planning;
    }

    #pragma warning disable CS8618
    private Session()
    {
    }
    #pragma warning restore

    public static Session CreateUnique(
        Planning planning,
        DateTime startDateTime,
        TimeSpan duration)
    {
        return new(
            new SessionId(Guid.NewGuid()),
            planning,
            startDateTime,
            duration
        );
    }
}
