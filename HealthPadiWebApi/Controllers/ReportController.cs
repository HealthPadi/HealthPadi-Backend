using HealthPadiWebApi.DTOs;
using HealthPadiWebApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealthPadiWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService reportService;

        public ReportController(IReportService reportService)
        {
            this.reportService = reportService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await reportService.GetAllReportsAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddReportDto addReportDto)
        {
            var report = await reportService.AddReportAsync(addReportDto);
            return CreatedAtAction(nameof(GetById), new { id = report.ReportId }, report);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var report = await reportService.GetReportByIdAsync(id);
            if (report == null)
            {
                return NotFound();
            }
            return Ok(report);
        }

        [HttpPatch]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateReportDto updateReportDto)
        {
            var report = await reportService.UpdateReportAsync(id, updateReportDto);
            return Ok(report);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var report = await reportService.DeleteReportAsync(id);
            if (report == null)
            {
                return NotFound();
            }
            return Ok(report);
        }
    }
}