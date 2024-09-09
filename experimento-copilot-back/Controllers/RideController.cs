using experimento_copilot_back.DTOs;
using experimento_copilot_back.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace experimento_copilot_back.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RideController : ControllerBase
    {
        private readonly IRideService _rideService;

        public RideController(IRideService rideService)
            => _rideService = rideService;

        [HttpPost("rides")]
        public async Task<IActionResult> AddRide(RideCreateDto rideCreateDto)
        {
            if (rideCreateDto.VehicleId == Guid.Empty || rideCreateDto.RiderId == Guid.Empty || rideCreateDto.Date == default)
            {
                return BadRequest("Todos os campos são obrigatórios.");
            }

            var result = await _rideService.AddRideAsync(rideCreateDto);

            if (!result)
            {
                return BadRequest("Não foi possível adicionar a carona.");
            }

            return Ok();
        }

        [HttpGet("rides/user/{userId}")]
        public async Task<IActionResult> GetRidesByUser(Guid userId)
        {
            var rides = await _rideService.GetRidesByUserId(userId);

            return Ok(rides);
        }
    }
}
