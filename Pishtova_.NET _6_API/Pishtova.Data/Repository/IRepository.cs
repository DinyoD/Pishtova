namespace Pishtova.Data.Common.Repository
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Pishtova.Data.Common.Model;
    using Pishtova.Data.Common.Utilities;
    using System;
    using System.Linq.Expressions;
    using System.Linq;

    public interface IRepository<TEntity,TKey>
        where TEntity : class, IEntity<TKey>
    {
        Task<OperationResult<bool>> AnyAsync(IEnumerable<Expression<Func<TEntity, bool>>> filters);

        Task<OperationResult<TEntity>> GetAsync(
            IEnumerable<Expression<Func<TEntity, bool>>> filters,
            IEnumerable<Func<IQueryable<TEntity>, IQueryable<TEntity>>> transforms
            );

        Task<OperationResult<IEnumerable<TEntity>>> GetManyAsync(
            IEnumerable<Expression<Func<TEntity, bool>>> filters,
            IEnumerable<Func<IQueryable<TEntity>, IQueryable<TEntity>>> transforms
            );

        Task<OperationResult> CreateAsync(TEntity entity);
        Task<OperationResult> UpdateAsync(TEntity entity);
        Task<OperationResult> DeleteAsync(TEntity entity);
    }
}
