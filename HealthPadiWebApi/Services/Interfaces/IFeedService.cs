using HealthPadiWebApi.DTOs;
using HealthPadiWebApi.Models;

namespace HealthPadiWebApi.Services.Interfaces
{
    public interface IFeedService
    {
        Task<List<FeedDto>> GetAllFeedsAsync();
        Task<FeedDto> AddFeedAsync(AddFeedDto addFeedDto);
    }
}
