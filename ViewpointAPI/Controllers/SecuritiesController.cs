using ViewpointAPI.Models;
using ViewpointAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ViewpointAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SecuritiesController : ControllerBase
{
    private readonly SecuritiesService _securitiesService;

    public SecuritiesController(SecuritiesService SecuritiesService) =>
        _securitiesService = SecuritiesService;

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Data>> Get(string id)
    {
        var security = await _securitiesService.GetAsync(id);

        if (security is null)
        {
            return NotFound();
        }

        return security;
    }

}
