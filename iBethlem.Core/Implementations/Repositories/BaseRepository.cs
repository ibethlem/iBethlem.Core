using iBethlem.Core.Abstractions.Repositories;
using iBethlem.Core.Data;
using iBethlem.Core.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace iBethlem.Core.Implementations.Repositories;

public abstract class BaseRepository(DataContext _context) : IBaseRepository
{
    public async Task<T> CreateEntityAsync<T>(T entity) where T : BaseEntity
    {
        entity.Created = DateTime.UtcNow;
        var created = _context.Set<T>().Attach(entity);
        _context.Entry(entity).State = EntityState.Added;
        await _context.SaveChangesAsync();
        return created.Entity;
    }

    public IQueryable<T> GetEntities<T>() where T : BaseEntity
    {
        return _context.Set<T>().AsQueryable();
    }

    public IQueryable<T> GetEntities<T>(Expression<Func<T, bool>> predictate) where T : BaseEntity
    {
        return _context.Set<T>().Where(predictate).AsQueryable();
    }
}
