using HealthPadiWebApi.DTOs;
using HealthPadiWebApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealthPadiWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    /* [Authorize(AuthenticationSchemes = "Bearer")]*/
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string? location)
        {
            var report = await _reportService.GetAllReportsAsync(location);
            if (report == null)
            {
                return NotFound();
            }
            return Ok(report);
        }
       /* [HttpGet]
        public async Task<IActionResult> Get1([FromQuery] string location)
        {
            // Check if location is provided
            if (string.IsNullOrEmpty(location))
            {
                return BadRequest("Location is required to retrieve reports.");
            }

            // Retrieve all reports for the specified location
            var reports = await _reportService.GetAllReportsAsync(location);

            // If no reports are found for the location, return a NotFound response
            if (reports == null || !reports.Any())
            {
                return NotFound("No reports found for the specified location.");
            }

            // Combine all report contents into a single string
            var combinedContent = string.Join(" ", reports.Select(r => r.Content));

            // Create a single string with location and combined content
            var combinedString = $"Location: {location}\n{combinedContent}";

            // Send the combined string to the AI for summarization (Assuming AI endpoint integration here)
            var aiResponse = await _aiService.SummarizeReportAsync(combinedString);

            // Return the AI's response to the user
            return Ok(aiResponse);
        }
*/


        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddReportDto addReportDto)
        {
            var report = await _reportService.AddReportAsync(addReportDto);
            return CreatedAtAction(nameof(GetById), new { id = report.ReportId }, report);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var report = await _reportService.GetReportByIdAsync(id);
            if (report == null)
            {
                return NotFound();
            }
            return Ok(report);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var report = await _reportService.DeleteReportAsync(id);
            if (report == null)
            {
                return NotFound();
            }
            return Ok(report);
        }
    }
}