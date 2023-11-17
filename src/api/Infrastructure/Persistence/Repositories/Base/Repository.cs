using System.Linq.Expressions;
using Application.Persistence.Base;
using Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories.Base;

internal class Repository<TEntity, TId> : IRepository<TEntity, TId>
    where TId : notnull
    where TEntity : Entity<TId>
{
    protected readonly AppDbContext _context;

    public Repository(AppDbContext context)
    {
        _context = context;
    }

    public virtual async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await _context
            .Set<TEntity>()
            .AddAsync(entity, cancellationToken);

        return entity;
    }

    public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _context.Set<TEntity>().AnyAsync(predicate, cancellationToken);
    }

    public virtual async Task<IReadOnlyList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _context
            .Set<TEntity>()
            .AsNoTracking()
            .Where(predicate)
            .ToArrayAsync(cancellationToken);
    }

    public virtual async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await GetAllAsync(e => e != null, cancellationToken);
    }

    public virtual async Task<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken = default)
    {
        return await _context
            .Set<TEntity>()
            .FirstOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);
    }
}
