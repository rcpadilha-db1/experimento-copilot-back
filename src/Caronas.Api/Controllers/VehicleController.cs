using Microsoft.AspNetCore.Mvc;
using Caronas.Domain;
using Caronas.Application.Contratos;

namespace Caronas.Api.Controllers
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

      // GET: api/vehicle
      [HttpGet]
      public async Task<IActionResult> Get()
      {
         try
         {
            var vehicle = await _vehicleService.GetAllVehiclesAsync();
            if(vehicle == null) return NotFound("Nenhum veículo encontrado.");
            
            return Ok(vehicle);
         }
         catch (Exception ex)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, 
               $"Erro ao tentar recuperar os veículos. Erro: {ex.Message}");
         }
      }

      // GET: api/vehicle/{id}
      [HttpGet("{id}")]
      public async Task<IActionResult> GetById(string id)
      {
         try
         {
            var vehicle = await _vehicleService.GetVehiclesByIdAsync(id);
            if(vehicle == null) return NotFound("Nenhum veículo encontrado.");
            
            return Ok(vehicle);
         }
         catch (Exception ex)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, 
               $"Erro ao tentar recuperar os veículos. Erro: {ex.Message}");
         }
      }

      // POST: api/vehicle
      [HttpPost]
      public async Task<IActionResult> Post( Vehicle model)
      {
         try
         {
            var vehicle = await _vehicleService.AddVehicle(model);
            if(vehicle == null) return BadRequest("Erro ao tentar adicionar veículo.");
            
            return Ok(vehicle);
         }
         catch (Exception ex)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, 
               $"Erro ao tentar adicionar o veículo. Erro: {ex.Message}");
         }
      }

      // PUT: api/vehicle/{id}
      [HttpPut("{id}")] 
      public async Task<IActionResult> Put(string id, Vehicle model)
      {
         try
         {
            var vehicle = await _vehicleService.UpdateVehicle(id, model);
            if(vehicle == null) return BadRequest("Erro ao tentar atualizar veículo.");
            
            return Ok(vehicle);
         }
         catch (Exception ex)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, 
               $"Erro ao tentar atualizar o veículo. Erro: {ex.Message}");
         }
      }

      // DELETE: api/vehicle/{id}
      [HttpDelete("{id}")]
      public async Task<IActionResult> Delete(string id)
      {
         try
         {
            return await _vehicleService.DeleteVehicle(id) ? 
               Ok("Veículo deletado.") : 
               BadRequest("Veículo não deletado.");
         }
         catch (Exception ex)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, 
               $"Erro ao tentar deletar o veículo. Erro: {ex.Message}");
         }
      }
   }
}
