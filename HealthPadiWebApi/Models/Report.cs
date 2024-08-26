namespace HealthPadiWebApi.Models
{
    public class Report
    {
        public Guid ReportId { get; set; }
        public Guid UserId { get; set; }
        public string Location { get; set; }
        public string Content { get; set; } 
        
        //NAVIGATION PROPERTIES
        public virtual User User { get; set; }
    }
}
