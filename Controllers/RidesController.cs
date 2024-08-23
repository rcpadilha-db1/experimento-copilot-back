using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SemCopilot.Data;
using SemCopilot.Models;

namespace SemCopilot.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RidesController : ControllerBase
    {
        private readonly SemCopilotDbContext _context;

        public RidesController(SemCopilotDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRide([FromBody] Ride ride)
        {
            if (ride == null || string.IsNullOrEmpty(ride.VehicleId) || string.IsNullOrEmpty(ride.RiderId) || ride.Date == default)
            {
                return BadRequest("Todos os campos são obrigatórios.");
            }

            var user = await _context.Users.FindAsync(ride.RiderId);
            if (user == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            // Verificar veiculo
            var vehicle = await _context.Vehicles.FindAsync(ride.VehicleId);
            if (vehicle == null)
            {
                return NotFound("Veículo não encontrado");
            }


            var existingRide = await _context.Rides
                .Where(r => r.RiderId == ride.RiderId && r.Date.Date == ride.Date.Date)
                .FirstOrDefaultAsync();

            if (existingRide != null)
            {
                return UnprocessableEntity("O usuário já tem uma viagem agendada para esta data.");
            }

            try
            {
                _context.Rides.Add(ride);
                await _context.SaveChangesAsync();
                return Ok(ride);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um erro ao criar a viagem.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRide(string id)
        {
            var ride = await _context.Rides.FindAsync(id);
            if (ride == null)
            {
                return NotFound("Passeio não encontrado.");
            }

            try
            {
                _context.Rides.Remove(ride);
                await _context.SaveChangesAsync();
                return Ok("Passeio excluído com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um erro ao excluir a viagem.");
            }
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetRidesByUser(string userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            var rides = await _context.Rides
                .Where(r => r.RiderId == userId)
                .Select(r => new
                {
                    Date = r.Date,
                    VehiclePlate = r.Vehicle.Plate,
                    OwnerName = r.Vehicle.Owner.Name
                })
                .ToListAsync();

            return Ok(rides);
        }
    }
}
