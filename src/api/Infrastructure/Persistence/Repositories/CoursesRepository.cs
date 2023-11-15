using System.Linq.Expressions;
using Application.Persistence;
using Domain.Entities;
using Domain.ValueObjects;
using Infrastructure.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

internal class CoursesRepository : Repository<Course, CourseId>, ICoursesRepository
{
    public CoursesRepository(AppDbContext context) : base(context)
    {
    }

    public override async Task<IReadOnlyList<Course>> GetAllAsync(Expression<Func<Course, bool>> predicate, CancellationToken cancellationToken = default)
    {
        var courses = await _context.Courses
            .Include(c => c.Planning)
            .Where(predicate)
            .ToArrayAsync(cancellationToken);

        return courses;
    }

#pragma warning disable CS8602
    public async Task<IReadOnlyList<Course>> GetAllScheduledCourses(Expression<Func<Course, bool>> predicate, CancellationToken cancellationToken = default)
    {
        var courses = await _context.Courses
            .Where(p => p.Planning != null)
            .Include(c => c.Planning)
            .ThenInclude(p => p.Definition)
            .ThenInclude(d => d.Items)
            .Where(predicate)
            .ToArrayAsync(cancellationToken);

        return courses;
    }

    public override async Task<Course?> GetByIdAsync(CourseId id, CancellationToken cancellationToken = default)
    {
        return await _context.Courses
            .Include(c => c.Planning)
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public async Task<Course?> GetByIdIncludingAllsAsync(CourseId id, CancellationToken cancellationToken = default)
    {
        return await _context.Courses
            .AsNoTracking()
            .Where(c => c.Planning != null)
            .Include(c => c.Planning)
            .ThenInclude(p => p.Sessions)
            .Include(p => p.Planning)
            .ThenInclude(p => p.Definition)
            .ThenInclude(d => d.Items)
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public async Task<Course?> GetByIdIncludingDefinitionsAsync(CourseId id, CancellationToken cancellationToken = default)
    {
        return await _context.Courses
            // .Where(c => c.Planning != null)
            .Include(c => c.Planning)
            .ThenInclude(p => p.Definition)
            .ThenInclude(d => d.Items)
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public async Task<Course?> GetByIdIncludingSessionsAsync(CourseId id, CancellationToken cancellationToken = default)
    {
        return await _context.Courses
            // .Where(c => c.Planning != null)
            .Include(c => c.Planning)
            .ThenInclude(p => p.Sessions)
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }
#pragma warning restore CS8602
}
