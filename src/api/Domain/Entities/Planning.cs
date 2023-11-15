using System.Collections.ObjectModel;
using Domain.Entities.Base;
using Domain.ValueObjects;

namespace Domain.Entities;

public sealed class Planning : Entity<PlanningId>
{
    public CourseId CourseId { get; set; }
    public Course Course { get; set; }
    public Definition Definition { get; set; }
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

    public Task<(int RemainingHours, ICollection<Session> Sessions)> GenerateSessionsAsync(DateOnly startDay, DateOnly endDay)
    {
        return Task.Run(() => 
        {
            Sessions.Clear();
            var definitions = Definition.Items;
            int totalHours = Course.TotalHours;
            DateOnly date = startDay;

            while (date < endDay && totalHours > 0)
            {
                // var temp = totalHours;
                foreach (var def in definitions)
                {
                    // if(totalHours - def.Duration.Hours < 0)
                    // {
                    //     break;
                    // }

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
                // if(temp == totalHours)
                //     break;

                date = date.AddDays(7);
            }

            return (totalHours, Sessions);
        });
    }
}
