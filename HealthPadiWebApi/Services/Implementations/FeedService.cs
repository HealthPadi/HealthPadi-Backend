using AutoMapper;
using HealthPadiWebApi.DTOs;
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

        public async Task<List<FeedDto>> GetAllFeedsAsync()
        {
            var feeds = await _unitOfWork.Feed.GetAll();
            return _mapper.Map<List<FeedDto>>(feeds);
        }
    }
}
