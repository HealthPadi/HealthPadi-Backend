using HealthPadiWebApi.Models;
using System.Security.Claims;

namespace HealthPadiWebApi.Services.Interfaces
{
    public interface IAIService
    {
        IAsyncEnumerable<string> ChatWithAI(ChatRequest request, CancellationToken cancellationToken);
        //Task<string> GenerateReportSummary(string prompt);
        Task<string> GenerateHealthFeeds();
    }
}
