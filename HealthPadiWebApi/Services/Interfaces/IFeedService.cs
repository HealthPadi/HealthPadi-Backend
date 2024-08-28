using HealthPadiWebApi.DTOs.Request;
using HealthPadiWebApi.DTOs.Response;
using HealthPadiWebApi.Models;

namespace HealthPadiWebApi.Services.Interfaces
{
    public interface IFeedService
    {
        Task<List<FeedDto>> GetAllFeedsAsync();
        Task<FeedDto> AddFeedAsync(AddFeedDto addFeedDto);
    }
}
