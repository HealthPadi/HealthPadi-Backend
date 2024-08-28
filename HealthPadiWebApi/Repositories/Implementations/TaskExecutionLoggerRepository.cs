using HealthPadiWebApi.Data;
using HealthPadiWebApi.Models;
using HealthPadiWebApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HealthPadiWebApi.Repositories.Implementations
{
    public class TaskExecutionLoggerRepository : GenericRepository<TaskExecutionLog>
    {
      protected DbSet<TaskExecutionLog> _dbSet;
        public TaskExecutionLoggerRepository(HealthPadiDataContext dbContext) : base(dbContext)
        {
            _dbSet = dbContext.Set<TaskExecutionLog>();
        }
    }
}
