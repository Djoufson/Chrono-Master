using System.Linq.Expressions;
using Application.Persistence.Base;
using Domain.Entities;
using Domain.ValueObjects;

namespace Application.Persistence;

public interface ICoursesRepository : IRepository<Course, CourseId>
{
    Task<IReadOnlyList<Course>> GetAllScheduledCourses(Expression<Func<Course, bool>> predicate, CancellationToken cancellationToken = default);
    Task<Course?> GetByIdIncludingDefinitionsAsync(CourseId id, CancellationToken cancellationToken = default);
    Task<Course?> GetByIdIncludingSessionsAsync(CourseId id, CancellationToken cancellationToken = default);
    Task<Course?> GetByIdIncludingAllsAsync(CourseId id, CancellationToken cancellationToken = default);
}
