using AutoMapper;
using HealthPadiWebApi.DTOs.Request;
using HealthPadiWebApi.DTOs.Response;
using HealthPadiWebApi.Models;
using HealthPadiWebApi.Repositories.Interfaces;
using HealthPadiWebApi.Services.Interfaces;

namespace HealthPadiWebApi.Services.Implementations
{
    public class FeedService : IFeedService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FeedService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<FeedDto> AddFeedAsync(AddFeedDto addFeedDto)
        {
            var feed = _mapper.Map<Feed>(addFeedDto);
            await _unitOfWork.Feed.AddAsync(feed);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<FeedDto>(feed);
        }
        public async Task<List<FeedDto>> GetAllFeedsAsync()
        {
            var feeds = await _unitOfWork.Feed.GetAll();
            return _mapper.Map<List<FeedDto>>(feeds);
        }
    }
}
