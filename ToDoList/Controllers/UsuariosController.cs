using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Models.DTOs;
using ToDoList.Services;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController(UsuarioService usuarioService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<UsuarioResponseDto>>> Get() =>
            Ok(await usuarioService.GetAllAsync());

        [HttpGet("id")]
        public async Task<ActionResult<UsuarioResponseDto>> GetById(Guid id)
        {
            var usuario = await usuarioService.GetByIdAsync(id);
            return usuario is not null ? Ok(usuario) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<UsuarioResponseDto>> Post(UsuarioCreateDto dto)
        {
            var novoUsuario = await usuarioService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new{id = novoUsuario.Id}, novoUsuario);
        }
    }
}