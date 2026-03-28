using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.Data;
using ToDoList.Models.DTOs;
using ToDoList.Services;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly AuthService _authService;

        public AuthController(AppDbContext context, AuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            //Busca pelo email
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == dto.Email);

            if (usuario == null || !BCrypt.Net.BCrypt.Verify(dto.Senha, usuario.PasswordHash))
            {
                return Unauthorized(new { message = "Email ou senha inválidos."});
            }

            var token = _authService.GerarToken(usuario);
            return Ok(new
            {
                token = token,
                usuario = new { usuario.Id, usuario.Nome, usuario.Email }
            });
        }
    }
}
