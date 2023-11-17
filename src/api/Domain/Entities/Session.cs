using Domain.Entities.Base;
using Domain.ValueObjects;

namespace Domain.Entities;

public sealed class Session : Entity<SessionId>, IComparable<Session>
{
    public Planning Planning { get; private set; }
    public DateTime StartDateTime { get; private set; }
    public TimeSpan Duration { get; private set; }
    public bool PendingUpdates { get; private set; }

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

    internal void ChangeSessionHour(DateTime startDateTime, TimeSpan duration)
    {
        StartDateTime = startDateTime;
        Duration = duration;
        ChangeStatus(true);
    }

    public void ResolvePendingUpdates()
    {
        ChangeStatus(false);
    }

    private void ChangeStatus(bool updated)
    {
        PendingUpdates = updated;
    }

    public int CompareTo(Session? other)
    {
        if (other == null)
            return 1;

        // Compare StartDateTime
        return StartDateTime.CompareTo(other.StartDateTime);
    }
}
