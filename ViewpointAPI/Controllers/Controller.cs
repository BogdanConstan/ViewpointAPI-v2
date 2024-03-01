using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ViewpointAPI.Models;
using ViewpointAPI.Services;
using ViewpointAPI.Exceptions;

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
            if (string.IsNullOrWhiteSpace(identifier))
            {
                return BadRequest("Identifier cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(field))
            {
                return BadRequest("Field cannot be empty.");
            }

            identifier = identifier.Trim().ToUpper();
            field = field.Trim().ToUpper();

            if (startDate.HasValue && endDate.HasValue && startDate > endDate)
            {
                return BadRequest("Start date cannot be after end date.");
            }

            try
            {
                var historyDataItems = await _historyService.GetHistory(identifier, field, startDate, endDate);

                var historyResponse = new HistoryResponse
                {
                    Identifier = identifier,
                    Field = field,
                    Data = historyDataItems
                };

                return Ok(historyResponse);
            }
            catch (IdNotFoundException ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

        [HttpGet("reference")]
        public async Task<ActionResult<ReferenceResponse>> GetReference(string identifier, string field)
        {
            // Parameter validation
            if (string.IsNullOrWhiteSpace(identifier))
            {
                return BadRequest("Identifier cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(field))
            {
                return BadRequest("Field cannot be empty.");
            }

            identifier = identifier.Trim().ToUpper();
            field = field.Trim().ToUpper();            

            try
            {
                var referenceData = await _referenceService.GetReference(identifier, field);

                var referenceResponse = new ReferenceResponse
                {
                    Identifier = identifier,
                    Field = field,
                    Value = referenceData
                };

                return Ok(referenceResponse);
            }
            catch (IdNotFoundException ex)
            {
                return StatusCode(400, ex.Message);
            }
        }
    }
}
