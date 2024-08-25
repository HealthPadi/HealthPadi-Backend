

using AutoMapper;
using HealthPadiWebApi.Models;
using HealthPadiWebApi.Repositories.Interfaces;
using HealthPadiWebApi.Services.Interfaces;

namespace HealthPadiWebApi.Services.Implementations
{
    public class TaskExecutionLoggerService : ITaskExecutionLoggerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        TaskExecutionLoggerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<TaskExecutionLog> AddTaskExecutionLog(TaskExecutionLog logToSave)
        {
            await _unitOfWork.TaskExecutionLogger.AddAsync(logToSave);
            await _unitOfWork.CompleteAsync();
            return logToSave;
        }

        public async Task<List<TaskExecutionLog>> GetAllTaskExecutionLogs()
        {
            var logs = await _unitOfWork.TaskExecutionLogger.GetAll();
            return _mapper.Map<List<TaskExecutionLog>>(logs);
        }
    }
}
