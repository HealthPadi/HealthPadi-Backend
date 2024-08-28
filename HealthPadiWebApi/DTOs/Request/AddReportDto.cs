namespace HealthPadiWebApi.DTOs.Request
{
    public class AddReportDto
    {
        public Guid UserId { get; set; }
        public string Location { get; set; }
        public string Content { get; set; }
    }
}
