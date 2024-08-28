namespace HealthPadiWebApi.Models
{
    public class ChatRequest {
        public List<ChatMessage> ChatHistory { get; set; } = [];
        public required string NewMessage { get; set; }

    }
}
