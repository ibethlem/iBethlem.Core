using iBethlem.Core.Abstractions.Mappers;
using iBethlem.Core.Abstractions.Repositories;
using iBethlem.Core.Data;
using iBethlem.Core.Models.Api.Responses;
using iBethlem.Core.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace iBethlem.Core.Implementations.Repositories;

public abstract class BaseRepository<TEntity, TModel>(IModelMapper<TEntity, TModel> _mapper, DataContext _context) : IBaseRepository<TEntity, TModel>, IDisposable where TEntity : BaseEntity where TModel : BaseResponse
{
    private bool disposedValue;

    public async Task<TModel> CreateEntityAsync(TModel model)
    {
        var entity = _mapper.MapToEntity(model);
        entity.Created = DateTime.UtcNow;
        entity.IsActive = true;
        var created = _context.Set<TEntity>().Attach(entity);
        _context.Entry(entity).State = EntityState.Added;
        await _context.SaveChangesAsync();
        return _mapper.MapToModel(created.Entity);
    }

    public Task<TModel> GetEntityAsync(int entityId)
    {
        return _context.Set<TEntity>().Where(entity => entity.IsActive && entity.Id == entityId).Select(entity => _mapper.MapToModel(entity)).FirstOrDefaultAsync();
    }

    public IQueryable<TModel> GetEntities()
    {
        return _context.Set<TEntity>().Where(entity => entity.IsActive).AsQueryable().Select(entity => _mapper.MapToModel(entity));
    }

    public IQueryable<TModel> GetEntities(Expression<Func<TEntity, bool>> predictate)
    {
        return _context.Set<TEntity>().Where(entity => entity.IsActive).Where(predictate).AsQueryable().Select(entity => _mapper.MapToModel(entity));
    }

    public async Task<TModel> UpdateEntityAsync(int entityId, TModel model)
    {
        var entity = await _context.Set<TEntity>().Where(entity => entity.IsActive && entity.Id == entityId).FirstOrDefaultAsync();
        var newEntity = _mapper.MapToEntity(model);
        _context.Entry(entity).CurrentValues.SetValues(newEntity);
        await _context.SaveChangesAsync();
        return _mapper.MapToModel(entity);
    }

    public async Task<TModel> DeleteEntityAsync(int entityId)
    {
        var entity = await _context.Set<TEntity>().Where(entity => entity.IsActive && entity.Id == entityId).FirstOrDefaultAsync();
        entity.IsActive = false;
        await _context.SaveChangesAsync();
        return _mapper.MapToModel(entity);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                _context.Dispose();
            }

            disposedValue = true;
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
