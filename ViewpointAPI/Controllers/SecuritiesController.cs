using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ViewpointAPI.Models;
using ViewpointAPI.Services;
using Microsoft.Extensions.Caching.Memory;

namespace ViewpointAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SecuritiesController : ControllerBase
    {
        private readonly DataService _dataService;
        //private readonly ReferenceService _referenceService;

        private readonly IMemoryCache _memoryCache;

        public SecuritiesController(DataService dataService, /*ReferenceService referenceService,*/ IMemoryCache memoryCache)
        {
            _dataService = dataService;
            //_referenceService = referenceService;
            _memoryCache = memoryCache;
            _memoryCache.Set("GCAN30YR INDEX", "BBG002SBNXC9");
        }

        [HttpGet("history")]
        public async Task<ActionResult<List<Data>>> GetHistory(string identifier, string field, DateTime? startDate, DateTime? endDate)
        {
             if (!_memoryCache.TryGetValue(identifier, out string globalIdentifier))
            {
                // Retrieve the global identifier based on the identifier from IDS
                //globalIdentifier = await SomeMethodToRetrieveGlobalIdentifier(identifier);

                if (globalIdentifier == null)
                {
                    return NotFound($"Global identifier not found for identifier: {identifier}");
                }

                // Cache the global identifier with the identifier as the cache key
                _memoryCache.Set(identifier, globalIdentifier);
            }

            var history = await _dataService.Get(globalIdentifier, field, startDate, endDate);

            if (history == null)
            {
                return NotFound();
            }

            return history;
        }

        /*[HttpGet("reference")]
        public async Task<ActionResult<List<Reference>>> GetReference(string identifier, string field)
        {
            var reference = await _referenceService.Get(identifier, field);

            if (reference == null)
            {
                return NotFound();
            }

            return reference;
        }*/
    }
}
