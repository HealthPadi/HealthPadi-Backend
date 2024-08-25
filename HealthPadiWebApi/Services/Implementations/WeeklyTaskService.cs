﻿
using HealthPadiWebApi.Models;
using HealthPadiWebApi.Services.Interfaces;

namespace HealthPadiWebApi.Services.Implementations
{
    public class WeeklyTaskService : BackgroundService
    {
        private readonly ILogger<WeeklyTaskService> _logger;
        private readonly IAIService _aiService;
        private readonly IFeedService _feedService;
        private readonly ITaskExecutionLoggerService _taskExecutionLoggerService;

        public WeeklyTaskService(ILogger<WeeklyTaskService> logger, IAIService aiService, ITaskExecutionLoggerService taskExecutionLoggerService, IFeedService feedService)
        {

            _logger = logger;
            _aiService = aiService;
            _feedService = feedService;
            _taskExecutionLoggerService = taskExecutionLoggerService;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await CheckAndRunMissedTask(stoppingToken);

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var currentDay = DateTime.Now.DayOfWeek;
                    var currentTime = DateTime.Now.TimeOfDay;

                    if (currentDay == DayOfWeek.Sunday && currentTime >= new TimeSpan(00, 00, 00) && currentTime < new TimeSpan(01, 00, 00))
                    {
                        await RunWeeklyTask(stoppingToken);
                        await _taskExecutionLoggerService.AddTaskExecutionLog(new TaskExecutionLog { TaskExecutionLogId = Guid.NewGuid(), LastExecutionTime = DateTime.Now });
                    }

                    await Task.Delay(TimeSpan.FromHours(1), stoppingToken);

                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Error occurred while Executing weekly task");
                }
            }
        }

        private async Task RunWeeklyTask(CancellationToken stoppingToken)
        {
            try
            {
                //call the AI service to generate health feeds
                var healthFeed = await _aiService.GenerateHealthFeeds();
                if (string.IsNullOrEmpty(healthFeed))
                {
                    _logger.LogWarning("No healthfeed generated by AI service");
                    return;
                }

                await _feedService.AddFeed(healthFeed);
                _logger.LogInformation("Weekly task executed and Healthfeed saved to DB");

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error occurred while running weekly task");
            }
        }

        private async Task CheckAndRunMissedTask(CancellationToken stoppingToken)
        {
            try
            {
                var logs = await _taskExecutionLoggerService.GetAllTaskExecutionLogs();

                var latestLog = logs.OrderByDescending(log => log.LastExecutionTime).FirstOrDefault();

                if (latestLog != null)
                {
                    var timeSinceLastExecution = DateTime.Now - latestLog.LastExecutionTime;
                    if (timeSinceLastExecution.TotalDays > 7)
                    {
                        await RunWeeklyTask(stoppingToken);
                    }
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error getting logged date for healthfeed");
            }
        }
    }
}
