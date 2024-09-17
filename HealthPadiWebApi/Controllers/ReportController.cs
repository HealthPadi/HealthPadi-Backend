using AutoMapper;
using HealthPadiWebApi.DTOs.Request;
using HealthPadiWebApi.DTOs.Response;
using HealthPadiWebApi.DTOs.Shared;
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
        private readonly IAIService _aiService;
        private readonly IMapper _mapper;

        public ReportController(IReportService reportService, IAIService aiService, IMapper mapper)
        {
            _reportService = reportService;
            _aiService = aiService;
            _mapper = mapper;
        }

        /**
         * 
         * Get - Retrieves all reports for a specified location and sends the combined content to the AI for summarization
         * @param location - the location to retrieve reports for
         * @return a response containing the AI's summary of the combined content
         */
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string location)
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
            var aiResponse = await _aiService.GenerateReportSummary(combinedString);

            // Return the AI's response to the user
            return Ok(ApiResponse.SuccessMessageWithData(aiResponse));
        }


        /**
         * Add - Adds a new report
         * @param addReportDto - the report to add
         * @return a response containing the added report
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
                return NotFound(ApiResponse.NotFoundException("Report not found"));
            }
            return Ok(ApiResponse.SuccessMessageWithData(_mapper.Map<ReportDto>(report)));
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var report = await _reportService.DeleteReportAsync(id);
            if (report == null)
            {
                return NotFound(ApiResponse.NotFoundException("Report not found"));
            }
            return Ok(ApiResponse.SuccessMessage("Report deleted!"));
        }
    }
}