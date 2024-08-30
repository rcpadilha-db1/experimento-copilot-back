using Microsoft.AspNetCore.Mvc;
using Caronas.Domain;
using Caronas.Application.Contratos;

namespace Caronas.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
   
   private readonly IUserService _userService;

   public UserController(IUserService userService)
   {
      _userService = userService;
   
   }

   [HttpGet]
   public async Task<IActionResult> Get()
   {
      try
      {
         var users = await _userService.GetAllUsersAsync();
         if(users == null) return NotFound("Nenhum usuário encontrado.");
         
         return Ok(users);
      }
      catch (Exception ex)
      {
         return StatusCode(StatusCodes.Status500InternalServerError, 
            $"Erro ao tentar recuperar os usuários. Erro: {ex.Message}");
      }
   }

   [HttpGet("{id}")]
   public async Task<IActionResult> GetById(string id)
   {
      try
      {
         var user = await _userService.GetUserByIdAsync(id);
         if(user == null) return NotFound("Nenhum usuário encontrado.");
         
         return Ok(user);
      }
      catch (Exception ex)
      {
         return StatusCode(StatusCodes.Status500InternalServerError, 
            $"Erro ao tentar recuperar os usuários. Erro: {ex.Message}");
      }
   }

   [HttpPost]
   public async Task<IActionResult> Post(User model)
   {
      try
      {
         var user = await _userService.AddUser(model);
         if(user == null) return BadRequest("Erro ao tentar adicionar usuário.");
         
         return Ok(user);
      }
      catch (Exception ex)
      {
         return StatusCode(StatusCodes.Status500InternalServerError, 
            $"Erro ao tentar adicionar o usuário. Erro: {ex.Message}");
      }
   }

   [HttpPut("{id}")]
   public async Task<IActionResult> Put(string id, User model)
   {
      try
      {
         var user = await _userService.UpdateUser(id, model);
         if(user == null) return BadRequest("Erro ao tentar atualizar usuário.");
         
         return Ok(user);
      }
      catch (Exception ex)
      {
         return StatusCode(StatusCodes.Status500InternalServerError, 
            $"Erro ao tentar atualizar o usuário. Erro: {ex.Message}");
      }
   }

   [HttpDelete("{id}")]
   public async Task<IActionResult> Delete(string id)
   {
      try
      {
         return await _userService.DeleteUser(id) ? 
            Ok("Usuário deletado.") : 
            BadRequest("Usuário não deletado.");
      }
      catch (Exception ex)
      {
         return StatusCode(StatusCodes.Status500InternalServerError, 
            $"Erro ao tentar deletar o usuário. Erro: {ex.Message}");
      }
   }
}
