namespace HealthPadiWebApi.Models
{
    public class Feed
    {
        public Guid FeedId { get; set; }
        public string FeedContent { get; set; }

        //Navigation Properties
        public virtual ICollection<FeedMedia> FeedMedias { get; set; } = new List<FeedMedia>();
    }
}
