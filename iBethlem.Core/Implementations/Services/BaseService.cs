using System.Linq.Expressions;
using iBethlem.Core.Abstractions.Repositories;
using iBethlem.Core.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace iBethlem.Core.Implementations.Services;

public class BaseService(IBaseRepository _baseRepository)
{
    protected async Task<bool> Exists<T>(Expression<Func<T, bool>> predicate) where T : BaseEntity
    {
        var entities = _baseRepository.GetEntities<T>();
        var existing = await entities.FirstOrDefaultAsync(predicate);
        return existing is not null;
    }

    protected async Task<T> CreateEntityAsync<T>(T entity, Expression<Func<T, bool>> predictate) where T : BaseEntity
    {
        if (await Exists(predictate))
        {
            return null;
        }

        return await _baseRepository.CreateEntityAsync(entity);
    }
}
