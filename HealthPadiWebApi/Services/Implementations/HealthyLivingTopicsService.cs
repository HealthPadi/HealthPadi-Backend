using AutoMapper;
using HealthPadiWebApi.Models;
using HealthPadiWebApi.Repositories.Interfaces;
using HealthPadiWebApi.Services.Interfaces;

namespace HealthPadiWebApi.Services.Implementations
{
    public class HealthyLivingTopicsService : IHealthyLivingTopicsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public HealthyLivingTopicsService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<string> GetOneTopic()
        {
            var topics = await _unitOfWork.HealthyLivingTopic.GetAll();

            if (topics == null || !topics.Any())
            {
                throw new Exception("No topics found");
            }
            
            var selectedTopic = topics.First();
            Console.WriteLine($"Number of Topics is: {topics.Count}");
            Console.WriteLine($"The selected Topic is: {selectedTopic.Topic}");

            var result = await DeleteSelectedTopic(selectedTopic.HealthyLivingTopicId);

            if (!result)
            {
                throw new Exception("couldn't delete topic");
            }

            await _unitOfWork.CompleteAsync();
            return selectedTopic.Topic;
        }

        public async Task<bool> DeleteSelectedTopic(Guid id)
        {
            var deletedData = await _unitOfWork.HealthyLivingTopic.DeleteAsync(id);
            if (deletedData == null)
            {
                return false;
            }
            await _unitOfWork.CompleteAsync();
            return true;   
        }
    }
}
