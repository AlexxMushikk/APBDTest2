using Test2C.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Test2C.DTOs;
using Test2C.Services;

namespace Test2C.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RacersController(IDbService db) : ControllerBase
{
    [HttpGet("{id}/participations")]
    public async Task<ActionResult<RacerParticipationsDto>> GetParticipations(int id)
    {
        try
        {
            var dto = await db.GetRacerParticipations(id);
            return Ok(dto);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
    [HttpPost]
    public async Task<IActionResult> CreateRacer([FromBody] RacerCreateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var newId = await db.AddRacerAsync(dto);
        return CreatedAtAction(nameof(GetParticipations),
            new { id = newId }, null);
    }
}