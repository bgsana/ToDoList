using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.Data;
using ToDoList.Models.DTOs;
using ToDoList.Services;
using Microsoft.AspNetCore.Authorization;


namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly AuthService _authService;
        private readonly UsuarioService _usuarioService;

        public AuthController(AppDbContext context, AuthService authService, UsuarioService usuarioService)
        {
            _context = context;
            _authService = authService;
            _usuarioService = usuarioService;
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

        [Authorize]
        [HttpGet("id")]
        public async Task<ActionResult<UsuarioResponseDto>> GetById(Guid id)
        {
            var usuario = await _usuarioService.GetByIdAsync(id);
            return usuario is not null ? Ok(usuario) : NotFound();
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UsuarioResponseDto>> Post(UsuarioCreateDto dto)
        {
            var novoUsuario = await _usuarioService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = novoUsuario.Id }, novoUsuario);
        }
    }
}
