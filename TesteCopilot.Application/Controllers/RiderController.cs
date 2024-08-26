using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TesteCopilot.Repository.AppContext;
using TesteCopilot.Domain.Riders;
using TesteCopilot.Repository.Models;
using TesteCopilot.Dtos;

[ApiController]
[Route("api/[controller]")]
public class RidersController : ControllerBase
{
    private readonly AppDatabaseContext _context;
    private readonly IRiderDomain _riderDomain;

    public RidersController(AppDatabaseContext context, IRiderDomain riderDomain)
    {
        _context = context;
        _riderDomain = riderDomain;
    }

    [HttpPost]
    public async Task<IActionResult> CreateRider([FromBody] RiderInsert riderInsert)
    {
        var rider = new Ride { UsuarioId = riderInsert.UsuarioId, VeiculoId = riderInsert.VeiculoId, Date = riderInsert.Date };
        try
        {
            if (!await _riderDomain.ValidateRiderAsync(rider.UsuarioId, rider.VeiculoId))
            {
                return BadRequest(new { Message = "Invalid user or vehicle data." });
            }

            _context.Riders.Add(rider);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetRider), new { id = rider.Id }, rider);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "An error occurred while processing your request.", Details = ex.Message });
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetRider(int id)
    {
        try
        {
            var rider = await _context.Riders.FindAsync(id);
            if (rider == null)
            {
                return NotFound(new { Message = "Rider not found." });
            }
            return Ok(rider);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "An error occurred while processing your request.", Details = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRider(int id)
    {
        try
        {
            var rider = await _context.Riders.FindAsync(id);
            if (rider == null)
            {
                return NotFound(new { Message = "Rider not found." });
            }

            _context.Riders.Remove(rider);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "An error occurred while processing your request.", Details = ex.Message });
        }
    }

    [HttpGet("byUser/{userId}")]
    public async Task<IActionResult> GetRidersByUserId(int userId)
    {
        try
        {
            var riders = await _context.Riders
                .Where(r => r.UsuarioId == userId)
                .Include(v => v.Vehicle)
                .ToListAsync();

            if (riders == null || riders.Count == 0)
            {
                return NotFound(new { Message = "No riders found for the given user ID." });
            }

            return Ok(riders);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "An error occurred while processing your request.", Details = ex.Message });
        }
    }
}