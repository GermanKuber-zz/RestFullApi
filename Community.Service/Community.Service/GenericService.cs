using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Community.Core.Interfaces.Repositorys;
using Community.Core.Interfaces.Services;
using Community.Core.Results;

namespace Community.Service
{
    public class GenericService<TEntity> : IGenericService<TEntity> where TEntity : class
    {
        private readonly IGenericRepository<TEntity> _genericRepository;

        public GenericService(IGenericRepository<TEntity> genericRepository)
        {
            _genericRepository = genericRepository;

        }

        public virtual async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            return await _genericRepository.GetAsync(filter, orderBy, includeProperties);
        }

        public virtual async Task<TEntity> GetByIdAsync(object id)
        {
            return await _genericRepository.GetByIdAsync(id);
        }

        public virtual async Task<ActionResult<TEntity>> InserAsync(TEntity entity)
        {
            try
            {
                _genericRepository.Insert(entity);
                await _genericRepository.SaveAsync();
                return new ActionResult<TEntity>(entity, ActionStatus.Created);
            }
            catch (Exception ex)
            {
                return new ActionResult<TEntity>(null, ActionStatus.Error, ex);
            }
        }

        public virtual async Task<ActionResult<TEntity>> DeleteAsync(object id)
        {

            try
            {
                if (await _genericRepository.DeleteAsync(id))
                {
                    await _genericRepository.SaveAsync();
                    return new ActionResult<TEntity>(null, ActionStatus.Deleted);
                }
                else
                    return new ActionResult<TEntity>(null, ActionStatus.NotFound);

            }
            catch (Exception ex)
            {
                return new ActionResult<TEntity>(null, ActionStatus.Error, ex);
            }
        }



        public virtual async Task<ActionResult<TEntity>> UpdateAsync(TEntity entityToUpdate)
        {
            try
            {
            
                _genericRepository.Update(entityToUpdate);
                var result = await _genericRepository.SaveAsync();
                if (result > 0)
                    return new ActionResult<TEntity>(null, ActionStatus.Updated);
                else
                    return new ActionResult<TEntity>(null, ActionStatus.NothingModified);

            }
            catch (Exception ex)
            {
                return new ActionResult<TEntity>(null, ActionStatus.Error, ex);
            }


        }
    }
}