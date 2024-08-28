using AutoMapper;
using HealthPadiWebApi.DTOs.Response;
using HealthPadiWebApi.DTOs.Shared;
using HealthPadiWebApi.Models;
using HealthPadiWebApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealthPadiWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedController : ControllerBase
    {
        private readonly IFeedService _feedService;
        private readonly IMapper _mapper;

        public FeedController(IFeedService feedService, IMapper mapper)
        {
            _feedService = feedService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var feeds = await _feedService.GetAllFeedsAsync();
            /*return Ok(feeds);*/
            return Ok(ApiResponse.SuccessMessageWithData(_mapper.Map<List<FeedDto>>(feeds)));
        }
    }
}
