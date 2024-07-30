using HealthPadiWebApi.Data;
using HealthPadiWebApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HealthPadiWebApi.Repositories.Implementations
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly HealthPadiDataContext dbContext;
        protected DbSet<T> dbSet;

        public GenericRepository(HealthPadiDataContext dbContext)
        {
            this.dbContext = dbContext;
            this.dbSet = dbContext.Set<T>();
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            await dbSet.AddAsync(entity);
            return entity;
        }

        public virtual async Task<List<T>> GetAll()
        {
            return await dbSet.ToListAsync();
        }

        public virtual async Task<T> DeleteAsync(Guid id)
        {
            var entity = await dbSet.FindAsync(id);
            if (entity == null)
            {
                return null;
            }
            dbSet.Remove(entity);
            return entity;
        }


        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            var entity = await dbSet.FindAsync(id);
            if (entity == null)
            {
                return null;
            }
            return entity;
        }
    }
}
