using Test2C.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Test2C.DTOs;
using Test2C.Services;

namespace Test2C.Controllers;

[ApiController]
[Route("api/track-races")]
public class TrackRacesController(IDbService db) : ControllerBase
{
    [HttpPost("participants")]
    public async Task<IActionResult> AddParticipants([FromBody] TrackRaceParticipationCreateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            await db.AddTrackRaceParticipations(dto);
            return Created();
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}