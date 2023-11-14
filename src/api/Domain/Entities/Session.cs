using Domain.Entities.Base;
using Domain.ValueObjects;

namespace Domain.Entities;

public class Session : Entity<SessionId>
{
    public Planning Planning { get; set; }
    public DateTime StartDateTime { get; set; }
    public TimeSpan Duration { get; set; }

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
