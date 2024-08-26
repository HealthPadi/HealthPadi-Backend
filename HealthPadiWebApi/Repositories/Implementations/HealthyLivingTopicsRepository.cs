using HealthPadiWebApi.Data;
using HealthPadiWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthPadiWebApi.Repositories.Implementations
{
    public class HealthyLivingTopicsRepository : GenericRepository<HealthyLivingTopic>
    {
        protected DbSet<HealthyLivingTopic> _dbSet;
        public HealthyLivingTopicsRepository(HealthPadiDataContext dbContext) : base(dbContext)
        {
            _dbSet = dbContext.Set<HealthyLivingTopic>();
        }
    }
}
