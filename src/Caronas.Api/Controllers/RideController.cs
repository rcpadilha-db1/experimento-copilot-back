using Microsoft.AspNetCore.Mvc;
using Caronas.Domain;
using Caronas.Application.Contratos;

namespace Caronas.Api.Controllers
{
   [ApiController]
   [Route("api/[controller]")]
   public class RideController : ControllerBase
   {
      private readonly IRideService _rideService;
      public RideController(IRideService rideService)
      {
         _rideService = rideService;
      }

      [HttpGet]
      public async Task<IActionResult> Get()
      {
         try
         {
            var rides = await _rideService.GetAllRidesAsync();
            if(rides == null) return NotFound("Nenhuma carona encontrada.");
            
            return Ok(rides);
         }
         catch (Exception ex)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, 
               $"Erro ao tentar recuperar as caronas. Erro: {ex.Message}");
         }
      }

      [HttpGet("{id}")]
      public async Task<IActionResult> Get(string id)
      {
         try
         {
            var ride = await _rideService.GetRideByIdAsync(id);
            if(ride == null) return NotFound("Nenhuma carona encontrado.");
            
            return Ok(ride);
         }
         catch (Exception ex)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, 
               $"Erro ao tentar recuperar as caronas. Erro: {ex.Message}");
         }
      }

      [HttpPost]
      public async Task<IActionResult> Post(Ride model)
      {
         try
         {
            var ride = await _rideService.AddRide(model);
            if(ride == null) return BadRequest("Erro ao tentar adicionar carona.");
            
            return Ok(ride);
         }
         catch (Exception ex)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, 
               $"Erro ao tentar adicionar a carona. Erro: {ex.Message}");
         }
      }

      [HttpPut("{id}")]
      public async Task<IActionResult> Put(string id, Ride model)
      {
         try
         {
            var ride = await _rideService.UpdateRide(id, model);
            if(ride == null) return BadRequest("Erro ao tentar atualizar carona.");
            
            return Ok(ride);
         }
         catch (Exception ex)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, 
               $"Erro ao tentar atualizar a carona. Erro: {ex.Message}");
         }
      }

      [HttpDelete("{id}")]
      public async Task<IActionResult> Delete(string id)
      {
         try
         {
            return await _rideService.DeleteRide(id) ? 
            Ok("Carona deletada.") : 
            BadRequest("Carona não deletada.");
         }
         catch (Exception ex)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, 
               $"Erro ao tentar deletar a carona. Erro: {ex.Message}");
         }
      }
   }
}