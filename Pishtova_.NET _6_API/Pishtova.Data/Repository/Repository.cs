namespace Pishtova.Data.Common.Repository
{
    using System;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Pishtova.Data.Common.Model;
    using Pishtova.Data.Common.Utilities;
    using Pishtova.Data.Common.Extensions;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using System.Linq.Expressions;

    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {
        private readonly PishtovaDbContext _db;

        public Repository(PishtovaDbContext db)
        {
            this._db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<OperationResult> CreateAsync(TEntity entity)
        {
            var operationResult = new OperationResult();
            if (operationResult.ValidateNotNull(entity) == false) return operationResult;
            try
            {
                await this._db.AddAsync(entity);
                await this._db.SaveChangesAsync();
            }
            catch (Exception e)
            {
                operationResult.AddException(e);
            }
            return operationResult;
        }

        public async Task<OperationResult> DeleteAsync(TEntity entity)
        {
            var operationResult = new OperationResult();
            if (operationResult.ValidateNotNull(entity) == false) return operationResult;

            try
            {
                this._db.Remove(entity);
                await this._db.SaveChangesAsync();
            }
            catch (Exception e)
            {
                operationResult.AddException(e);
            }

            return operationResult;
        }



        public async Task<OperationResult<TEntity>> GetAsync(
            IEnumerable<Expression<Func<TEntity, bool>>> filters,
            IEnumerable<Func<IQueryable<TEntity>, IQueryable<TEntity>>> transforms
            )
        {
            var operationResult = new OperationResult<TEntity>();

            try
            {
                var result = await this._db.Set<TEntity>()
                    .Filter(filters)
                    .Transform(transforms)
                    .FirstOrDefaultAsync();
                operationResult.Data = result;
            }
            catch (Exception e)
            {
                operationResult.AddException(e);
            }
            return operationResult;
        }

        public async Task<OperationResult<IEnumerable<TEntity>>> GetManyAsync(
            IEnumerable<Expression<Func<TEntity, bool>>> filters,
            IEnumerable<Func<IQueryable<TEntity>, IQueryable<TEntity>>> transforms
            )
        {
            var operationResult = new OperationResult<IEnumerable<TEntity>>();

            try
            {
                var result = await this._db.Set<TEntity>()
                    .Filter(filters)
                    .Transform(transforms)
                    .ToListAsync();
                operationResult.Data = result;
            }
            catch (Exception e)
            {

                operationResult.AddException(e);
            }
            return operationResult;
        }

        public Task<OperationResult> UpdateAsync(TEntity entity)
        {
            throw new System.NotImplementedException();
        }

        public async Task<OperationResult<bool>> AnyAsync(
            IEnumerable<Expression<Func<TEntity, bool>>> filters
            )
        {
            var operationResult = new OperationResult<bool>();

            try
            {
                var result = await this._db.Set<TEntity>()
                    .Filter(filters)
                    .AnyAsync();
                operationResult.Data = result;
            }
            catch (Exception e)
            {
                operationResult.AddException(e);
            }
            return operationResult;
        }
    }
}
