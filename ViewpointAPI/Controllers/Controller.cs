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
            var historyResponse = await _historyService.GetHistory(identifier, field, startDate, endDate);
            return Ok(historyResponse);
        }

        [HttpGet("reference")]
        public async Task<ActionResult<ReferenceResponse>> GetReference(string identifier, string field)
        {
            var referenceResponse = await _referenceService.GetReference(identifier, field);
            return Ok(referenceResponse);
        }
    }
}
