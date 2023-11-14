using System.Linq.Expressions;
using Application.Persistence.Base;
using Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories.Base;

internal class Repository<TEntity, TId> : IRepository<TEntity, TId>
    where TId : notnull
    where TEntity : Entity<TId>
{
    private readonly AppDbContext _context;

    public Repository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await _context
            .Set<TEntity>()
            .AddAsync(entity, cancellationToken);

        return entity;
    }

    public async Task<IReadOnlyList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _context
            .Set<TEntity>()
            .AsNoTracking()
            .Where(predicate)
            .ToArrayAsync(cancellationToken);
    }

    public async Task<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken = default)
    {
        return await _context
            .Set<TEntity>()
            .FirstOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);
    }
}
