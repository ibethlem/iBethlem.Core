using iBethlem.Core.Models.Api.Responses;
using iBethlem.Core.Models.Entities;

namespace iBethlem.Core.Abstractions.Mappers;

public interface IModelMapper<TEntity, TModel> where TEntity : BaseEntity where TModel : BaseResponse
{
    TModel MapToModel(TEntity entity);
    TEntity MapToEntity(TModel model);
}