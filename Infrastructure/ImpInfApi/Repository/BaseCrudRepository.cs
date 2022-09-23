using ImpInfCommon.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ImpInfApi.Repository
{
    public class BaseCrudRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly AppDbContext dbContext;
        private readonly DbSet<TEntity> dbSet;

        public BaseCrudRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
            dbSet = dbContext.Set<TEntity>();
        }

        public virtual Task Create(TEntity entities)
        {
            return Create(new[] { entities });
        }

        public virtual async Task Create(TEntity[] entities)
        {
            foreach (var entity in entities)
            {
                dbContext.Attach(entity);
                MarkModified(entity);
            }

            dbSet.AddRange(entities);
            await dbContext.SaveChangesAsync();

            foreach (var entity in entities)
            {
                dbContext.Entry(entity).State = EntityState.Detached;
            }
        }

        public virtual Task<TEntity> ReadFirst(Expression<Func<TEntity, bool>> query, params Expression<Func<TEntity, object>>[] includedProperties)
        {
            return IncludProperties(includedProperties).FirstOrDefaultAsync(query);
        }

        public virtual async Task<TEntity[]> Read(Func<TEntity, bool> query = null, params Expression<Func<TEntity, object>>[] includedProperties)
        {
            return query != null ? IncludProperties(includedProperties).Where(query).ToArray() : IncludProperties(includedProperties).ToArray();
        }

        public virtual async Task Update(TEntity entity)
        {
            dbContext.Attach(entity);
            MarkModified(entity);
            dbSet.Update(entity);
            await dbContext.SaveChangesAsync();
            dbContext.Entry(entity).State = EntityState.Detached;
        }

        public virtual Task Delete(int id)
        {
            return Delete(entity => entity.Id == id);
        }

        public virtual async Task Delete(Func<TEntity, bool> query)
        {
            dbSet.RemoveRange(dbSet.Where(query));
            await dbContext.SaveChangesAsync();
        }


        protected void MarkModified(TEntity entity)
        {
            var collections = dbContext.Entry(entity).Collections;
            foreach (var collection in collections)
            {
                if (collection.CurrentValue == null) continue;
                foreach (var element in collection.CurrentValue)
                {
                    if (dbContext.Entry(element).State == EntityState.Unchanged)
                    {
                        dbContext.Entry(element).State = EntityState.Modified;
                    }
                }
            }

            foreach (var element in dbContext.Entry(entity).References)
            {
                if (element.CurrentValue == null)
                {
                    continue;
                }

                if (dbContext.Entry(element.CurrentValue).State == EntityState.Unchanged)
                {
                    dbContext.Entry(element.CurrentValue).State = EntityState.Modified;
                }
            }
        }

        protected IQueryable<TEntity> IncludProperties(Expression<Func<TEntity, object>>[] includeProperties)
        {
            return includeProperties.Aggregate(dbSet.AsNoTracking(), (query, includeProperty) => query.Include(includeProperty));
        }
    }
}
