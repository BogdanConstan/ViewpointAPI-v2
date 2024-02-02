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
    public SecuritiesController(DataService DataService) =>
        _dataService = DataService;

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Data>> Get(string id)
    {
        var history = await _dataService.GetData(id);

        if (history is null)
        {
            return NotFound();
        }

        return history;
    }

}
