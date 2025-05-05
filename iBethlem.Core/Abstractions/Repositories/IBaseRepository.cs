using iBethlem.Core.Models.Api.Responses;
using iBethlem.Core.Models.Entities;
using System.Linq.Expressions;

namespace iBethlem.Core.Abstractions.Repositories;

public interface IBaseRepository<TEntity, TModel> where TEntity : BaseEntity where TModel : BaseResponse
{
    Task<TModel> CreateEntityAsync(TModel model);
    Task<TModel> GetEntityAsync(int entityId);
    IQueryable<TModel> GetEntities();
    IQueryable<TModel> GetEntities(Expression<Func<TEntity, bool>> predictate);
    Task<TModel> UpdateEntityAsync(int entityId, TModel model);
    Task<TModel> DeleteEntityAsync(int entityId);
}
