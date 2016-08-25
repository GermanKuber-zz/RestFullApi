using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Community.Core.Results;

namespace Community.Core.Interfaces.Services
{
    public interface IGenericService<TEntity> where TEntity : class
    {
        Task<ActionResult<TEntity>> DeleteAsync(object id);
        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
        Task<TEntity> GetByIdAsync(object id);
        Task<ActionResult<TEntity>> InserAsync(TEntity entity);
        Task<IQueryable<TEntity>> GetAllAsync();
        Task<ActionResult<TEntity>> UpdateAsync(TEntity entityToUpdate);
    }
}