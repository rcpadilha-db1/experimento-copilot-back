using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TesteCopilot.Dtos;
using TesteCopilot.Repository.AppContext;
using TesteCopilot.Repository.Models;

namespace TesteCopilot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDatabaseContext _context;

        public UserController(AppDatabaseContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUsuario([FromBody] UserInsert userInsert)
        {
            var user = new User { Name = userInsert.Name, Email = userInsert.Email };
            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetUsuario), new { id = user.Id }, new { user.Name, user.Email });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while processing your request.", Details = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsuario(int id)
        {
            try
            {
                var usuario = await _context.Users.FindAsync(id);
                if (usuario == null)
                {
                    return NotFound(new { Message = "User not found." });
                }
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while processing your request.", Details = ex.Message });
            }
        }
    }
}
