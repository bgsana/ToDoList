using Microsoft.AspNetCore.Mvc;
using ToDoList.Models.DTOs;
using ToDoList.Services;

namespace ToDoList.Controllers
{
    [ApiController]
    [Route("api/tarefas/{tarefaId}/comentarios")]
    public class ComentariosController : ControllerBase
    {
        private readonly ComentarioService _service;

        public ComentariosController(ComentarioService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CriarComentario(Guid tarefaId, ComentarioCreateDto dto)
        {
            var comentario = await _service.AdicionarComentario(tarefaId, dto);
            return Ok(comentario);
        }

        [HttpGet]
        public async Task<IActionResult> ListarComentarios(Guid tarefaId)
        {
            var comentarios = await _service.ListarComentarios(tarefaId);
            return Ok(comentarios);
        }
    }
}