using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TesteCopilot.Dtos;
using TesteCopilot.Repository.AppContext;
using TesteCopilot.Repository.Models;

namespace TesteCopilot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly AppDatabaseContext _context;

        public VehicleController(AppDatabaseContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateVeiculo([FromBody] VehicleInsert vehicleInsert)
        {
            var vehicle = new Vehicle { Plate = vehicleInsert.Plate, Capacity = vehicleInsert.Capacity, OwenerId = vehicleInsert.OwenerId };

            {
                try
                {
                    await _context.Vehicles.AddAsync(vehicle);
                    await _context.SaveChangesAsync();
                    return CreatedAtAction(nameof(GetVeiculo), new { id = vehicle.Id }, vehicle);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { Message = "An error occurred while processing your request.", Details = ex.Message });
                }
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVeiculo(int id)
        {
            try
            {
                var veiculo = await _context.Vehicles.FindAsync(id);
                if (veiculo == null)
                {
                    return NotFound(new { Message = "Vehicle not found." });
                }
                return Ok(veiculo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while processing your request.", Details = ex.Message });
            }
        }
    }
}
