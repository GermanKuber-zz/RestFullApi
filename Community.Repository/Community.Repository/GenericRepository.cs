using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Community.Core.Interfaces.Context;
using Community.Core.Interfaces.Repositorys;
using Community.Repository.Context;

namespace Community.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        internal CommunityContext Context;
        internal DbSet<TEntity> DbSet;

        public GenericRepository(ICommunityContext context)
        {
            this.Context = ((CommunityContext)context);
            this.DbSet = ((CommunityContext)context).Set<TEntity>();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }
        public virtual async Task<IQueryable<TEntity>> GetAllAsync()
        {

            return DbSet;
        }
        public virtual Task<TEntity> GetByIdAsync(object id)
        {
            return DbSet.FindAsync(id);
        }

        public virtual void Insert(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public virtual async Task<bool> DeleteAsync(object id)
        {
            TEntity entityToDelete = await DbSet.FindAsync(id);
            if (entityToDelete == null)
                return false;
            Delete(entityToDelete);
            return true;
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (Context.Entry(entityToDelete).State == EntityState.Detached)
            {
                DbSet.Attach(entityToDelete);
            }
            DbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
    
            if (Context.Entry(entityToUpdate).State == EntityState.Detached)
                DbSet.Attach(entityToUpdate);
            Context.Entry(entityToUpdate).State = EntityState.Modified;
        }
        public virtual async Task<int> SaveAsync()
        {
            return await this.Context.SaveChangesAsync();
        }
    }
}
