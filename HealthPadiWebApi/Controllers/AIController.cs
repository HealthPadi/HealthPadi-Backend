using HealthPadiWebApi.Models;
using HealthPadiWebApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;

namespace HealthPadiWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AIController : ControllerBase
    {
        private readonly IAIService _aiService;
        public AIController(IAIService aiService)
        {
            _aiService = aiService;
        }

       /* [HttpPost]
        [Route("health-feeds")]
        public async Task<IActionResult> GenerateHealthFeeds()
        {
            var response =  await _aiService.GenerateHealthFeeds();
            return Ok(response);
        }*/

        [HttpPost]
        [Route("ai-chat-bot")]
        public async Task ChatWithBot([FromBody] ChatRequest request, CancellationToken cancellationToken)
        {
            var responseStream = Response.Body;
            bool hasResponse = false;

            try
            {
                await foreach (var chunk in _aiService.ChatWithAI(request, cancellationToken))
                {
                    hasResponse = true;
                    var chunkBytes = Encoding.UTF8.GetBytes(chunk);
                    await responseStream.WriteAsync(chunkBytes, 0, chunkBytes.Length, cancellationToken);
                    await responseStream.FlushAsync(cancellationToken);
                }

                if (!hasResponse)
                {
                    Response.StatusCode = StatusCodes.Status404NotFound;
                    var notFoundBytes = Encoding.UTF8.GetBytes("No response generated for the prompt.");
                    await responseStream.WriteAsync(notFoundBytes, 0, notFoundBytes.Length, cancellationToken);
                    await responseStream.FlushAsync(cancellationToken);
                }
            }
            catch (Exception ex)
            {
                // Log the exception (if logging is set up)
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                var errorBytes = Encoding.UTF8.GetBytes("An error occurred while processing the request.");
                await responseStream.WriteAsync(errorBytes, 0, errorBytes.Length, cancellationToken);
                await responseStream.FlushAsync(cancellationToken);
            }
        }

    }

}
