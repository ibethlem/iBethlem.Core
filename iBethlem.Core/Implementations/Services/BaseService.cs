using System.Linq.Expressions;
using iBethlem.Core.Abstractions.Repositories;
using iBethlem.Core.Models.Api.Responses;
using iBethlem.Core.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace iBethlem.Core.Implementations.Services;

public class BaseService<TEntity, TModel>(IBaseRepository<TEntity, TModel> _baseRepository) where TEntity : BaseEntity where TModel : BaseResponse
{
    protected async Task<bool> Exists(Expression<Func<TEntity, bool>> predicate)
    {
        var entities = _baseRepository.GetEntities(predicate);
        var existing = await entities.FirstOrDefaultAsync();
        return existing is not null;
    }

    protected async Task<ApiResponse<TModel>> CreateAsync(TModel entity, Expression<Func<TEntity, bool>> predictate)
    {
        if (await Exists(predictate))
        {
            return new("An entity with the same properties already exists.");
        }

        var created = await _baseRepository.CreateEntityAsync(entity);
        if (created is null || created.Id == 0)
        {
            return new("An error occurred while creating the entity.");
        }

        return new(created);
    }
}
