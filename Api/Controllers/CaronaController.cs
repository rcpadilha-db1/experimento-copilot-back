using Crosscutting.Requests;
using Domain.Caronas.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CaronaController : ControllerBase
{
   [HttpPost]
   public async Task<IActionResult> Cadastrar(
      [FromBody] CaronaRequest request,
      [FromServices] ICadastroCaronaService service)
   {
      var result = await service.CadastrarAsync(request);
      return Ok(result);
   }
   
   [HttpDelete("{caronaId}")]
   public async Task<IActionResult> Cadastrar(
      [FromRoute] string caronaId,
      [FromServices] IRemocaoCaronaService service)
   {
      await service.RemoverAsync(caronaId);
      return Ok();
   }
   
   [HttpGet("/usuario/{usuarioId}")]
   public async Task<IActionResult> Cadastrar(
      [FromRoute] string usuarioId,
      [FromServices] IListagemCaronaService service)
   {
      var caronas = await service.ListarCaronasAsync(usuarioId);
      return Ok(caronas);
   }
}