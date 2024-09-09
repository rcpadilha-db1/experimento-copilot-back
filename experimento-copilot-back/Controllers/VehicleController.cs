using experimento_copilot_back.DTOs;
using experimento_copilot_back.Entities;
using experimento_copilot_back.Interfaces.Services;
using experimento_copilot_back.Services;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace experimento_copilot_back.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;

        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateVehicle([FromBody] VehicleDto vehicleDto)
        {
            try
            {
                var vehicle = vehicleDto.Adapt<Vehicle>();
                await _vehicleService.CreateVehicleAsync(vehicle);
                return Ok(new { message = "Veículo criado com sucesso!" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Erro interno no servidor" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetVehicle()
        {
            var vehicle = await _vehicleService.GetAllVehiclesAsync();

            if (vehicle == null || !vehicle.Any())
            {
                return NotFound(new { message = "Nenhum veículo encontrado." });
            }

            return Ok(vehicle);
        }
    }
}
