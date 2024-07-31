namespace HealthPadiWebApi.Models
{
    public class FeedMedia
    {
        public Guid FeedMediaId { get; set; }
        public Guid FeedId { get; set; }
        public string? MediaUrl { get; set; }
        public string? MimeType { get; set; }
        public string? Extension { get; set; }

        //NAVIGATION PROPERTIES
        public virtual Feed Feed { get; set; }

    }
}
