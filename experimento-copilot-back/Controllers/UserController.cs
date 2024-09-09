using experimento_copilot_back.DTOs;
using experimento_copilot_back.Entities;
using experimento_copilot_back.Interfaces.Services;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace experimento_copilot_back.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserDto userDto)
        {
            try
            {
                var user = userDto.Adapt<User>();
                await _userService.CreateUserAsync(user);
                return Ok(new { message = "Usuário criado com sucesso!" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetAllUsersAsync();

            if (users == null || !users.Any())
            {
                return NotFound(new { message = "Nenhum usuário encontrado." });
            }

            return Ok(users);
        }
    }

}
