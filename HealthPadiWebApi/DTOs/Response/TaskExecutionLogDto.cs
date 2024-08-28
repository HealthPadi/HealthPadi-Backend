namespace HealthPadiWebApi.DTOs.Response
{
    public class TaskExecutionLogDto
    {
        public Guid TaskExecutionLogId { get; set; }
        public DateTime LastExecutionTime { get; set; }
    }
}
