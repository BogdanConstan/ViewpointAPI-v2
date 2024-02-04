using ViewpointAPI.Models;
using ViewpointAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ViewpointAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SecuritiesController : ControllerBase
{
    private readonly DataService _dataService;


    // Sample code. 
    // We would need two controller methods, one for history and one for reference
    public SecuritiesController(DataService DataService) =>
        _dataService = DataService;
    [HttpGet("{id}")]
    public async Task<ActionResult<List<Data>>> GetData(string identifier, string field, DateTime? startDate, DateTime? endDate)
    {
        var history = await _dataService.Get(identifier, field, startDate, endDate);

        if (history is null)
        {
            return NotFound();
        }

        return history;
    }

}
