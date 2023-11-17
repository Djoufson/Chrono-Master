using System.Collections.ObjectModel;
using Domain.Entities.Base;
using Domain.ValueObjects;

namespace Domain.Entities;

public sealed class Planning : Entity<PlanningId>
{
    public CourseId CourseId { get; private set; }
    public Course Course { get; private set; }
    public Definition Definition { get; private set; }
    public ICollection<Session> Sessions { get; private set; } = new Collection<Session>();
    private Planning(
        PlanningId id,
        Course course,
        Definition definition) : base(id)
    {
        Definition = definition;
        Course = course;
        CourseId = course.Id;
    }

    #pragma warning disable CS8618
    private Planning()
    {
    }
    #pragma warning restore

    public static Planning CreateUnique(
        Course course,
        Definition definition)
    {
        return new(
            new PlanningId(Guid.NewGuid()),
            course,
            definition
        );
    }

    public Task<(int RemainingHours, ICollection<Session> Sessions)> GenerateSessionsAsync(DateOnly startDay, DateOnly endDay, int skipNumber = 0)
    {
        return Task.Run(() => 
        {
            var turns = 0;
            if(skipNumber > 0)
            {
                var skippedSessions = Sessions
                    .Order()
                    .Skip(skipNumber)
                    .ToArray();

                Sessions.Clear();
                foreach (var session in skippedSessions)
                    Sessions.Add(session);
            }
            else
                Sessions.Clear();

            var definitions = Definition.Items
                .Order()
                .ToArray();

            int totalHours = Course.TotalHours;
            DateOnly date = startDay;

            while(turns++ < skipNumber)
                date = date.AddDays(7);

            while (date < endDay && totalHours > 0)
            {
                foreach (var def in definitions.OrderBy(d => d.DayOfWeek))
                {
                    var variation = (int)def.DayOfWeek - (int)date.DayOfWeek;
                    var day = date.Day;
                    if(variation > 0)
                        day += variation;

                    Sessions.Add(Session.CreateUnique(
                        this,
                        new DateTime(date.Year, date.Month, day, def.StartTime.Hour, def.StartTime.Minute, 0),
                        def.Duration));

                    totalHours -= def.Duration.Hours;
                }

                date = date.AddDays(7);
            }

            return (totalHours, Sessions);
        });
    }

    public async Task ChangeSessionHourAsync(Session session, DateTime startDateTime, TimeSpan duration, bool @override = false)
    {
        var initialStartDateTime = session.StartDateTime;

        // Change the related data
        session.ChangeSessionHour(startDateTime, duration);

        // Override if requested
        if(!@override)
            return;

        var sessions = Sessions.Order().ToArray();
        var firstSession = sessions.First();
        var lastSession = sessions.Last();
        var initDate = new DateOnly(initialStartDateTime.Year, initialStartDateTime.Month, initialStartDateTime.Day); // The initial startDate of the session
        var start = new DateOnly(firstSession.StartDateTime.Year, firstSession.StartDateTime.Month, firstSession.StartDateTime.Day); // The beginning of the first session
        var end = new DateOnly(lastSession.StartDateTime.Year, lastSession.StartDateTime.Month, lastSession.StartDateTime.Day); // The beginning of the last session

        // Get the skipNumber
        var skipNumber = (int)Math.Floor((initDate.DayNumber - start.DayNumber) / 7.0);
        _ = await GenerateSessionsAsync(start, end, skipNumber);
    }
}
