using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ViewpointAPI.Models;
using ViewpointAPI.Services;

namespace ViewpointAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class Controller : ControllerBase
    {
        private readonly IHistoryService _historyService;
        private readonly IReferenceService _referenceService;

        public Controller(IHistoryService historyService, IReferenceService referenceService)
        {
            _historyService = historyService ?? throw new ArgumentNullException(nameof(historyService));
            _referenceService = referenceService ?? throw new ArgumentNullException(nameof(referenceService));
        }

        [HttpGet("history")]
        public async Task<ActionResult<HistoryResponse>> GetHistory(string identifier, string field, DateTime? startDate, DateTime? endDate)
        {
            // Parameter validation
            if (string.IsNullOrEmpty(identifier))
            {
                return BadRequest("Identifier cannot be empty.");
            }

            if (string.IsNullOrEmpty(field))
            {
                return BadRequest("Field cannot be empty.");
            }

            if (startDate.HasValue && endDate.HasValue && startDate > endDate)
            {
                return BadRequest("Start date cannot be after end date.");
            }

            var historyResponse = await _historyService.GetHistory(identifier, field, startDate, endDate);

            if (historyResponse.Identifier == null)
            {
                return NotFound("History not found for the specified identifier.");
            }
            return Ok(historyResponse);
        }

        [HttpGet("reference")]
        public async Task<ActionResult<ReferenceResponse>> GetReference(string identifier, string field)
        {
            // Parameter validation
            if (string.IsNullOrEmpty(identifier))
            {
                return BadRequest("Identifier cannot be empty.");
            }

            if (string.IsNullOrEmpty(field))
            {
                return BadRequest("Field cannot be empty.");
            }

            var referenceResponse = await _referenceService.GetReference(identifier, field);

            if (referenceResponse.Identifier == null)
            {
                return NotFound("Reference not found for the specified identifier.");
            }

            return Ok(referenceResponse);
        }
    }
}
