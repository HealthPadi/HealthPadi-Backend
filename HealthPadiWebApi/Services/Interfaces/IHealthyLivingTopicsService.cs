using HealthPadiWebApi.Models;

namespace HealthPadiWebApi.Services.Interfaces
{
    public interface IHealthyLivingTopicsService
    {
        Task<string> GetOneTopic();
        Task<bool> DeleteSelectedTopic(Guid id);
    }
}
