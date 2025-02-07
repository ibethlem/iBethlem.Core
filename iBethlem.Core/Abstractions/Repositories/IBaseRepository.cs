using iBethlem.Core.Models.Entities;
using System.Linq.Expressions;

namespace iBethlem.Core.Abstractions.Repositories;

public interface IBaseRepository
{
    Task<T> CreateEntityAsync<T>(T entity) where T : BaseEntity;
    IQueryable<T> GetEntities<T>() where T : BaseEntity;
    IQueryable<T> GetEntities<T>(Expression<Func<T, bool>> predictate) where T : BaseEntity;
}
