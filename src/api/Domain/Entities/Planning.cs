using Domain.Entities.Base;
using Domain.ValueObjects;

namespace Domain.Entities;

public sealed class Planning : Entity<PlanningId>
{
    public CourseId CourseId { get; set; }
    public Course Course { get; set; }
    public Definition Definition { get; set; }
    public IReadOnlyList<Session> Sessions { get; private set; } = Array.Empty<Session>();
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

    public void GenerateSessions()
    {
        Sessions = Array.Empty<Session>();
    }
}
