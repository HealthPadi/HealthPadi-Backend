
namespace HealthPadiWebApi.DTOs
{
    public class ReportDto
    {
        public Guid ReportId { get; set; }
        public Guid UserId { get; set; }
        public string Location { get; set; }
        public string Content { get; set; }
    }
}
