using HealthPadiWebApi.Models;

namespace HealthPadiWebApi.Services.Interfaces
{
    public interface ITaskExecutionLoggerService
    {
        Task<TaskExecutionLog> AddTaskExecutionLog(TaskExecutionLog logToSave);
        Task<List<TaskExecutionLog>> GetAllTaskExecutionLogs();
    }
}
