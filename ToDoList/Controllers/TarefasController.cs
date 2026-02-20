using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Models.DTOs;
using ToDoList.Services;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefasController : ControllerBase
    {
        private readonly TarefaService _service;

        public TarefasController(TarefaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] bool? concluida)
        {
            var items = await _service.GetAllAsync(concluida);
            return Ok(items.Select(t => t.ToResponse()));
        }

        [HttpGet("{id: guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var tarefa = await _service.GetByIdAsync(id);
            if(tarefa is null)
                return NotFound(new {message = "Tarefa não encontrada"});

            return Ok(tarefa.ToResponse());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TarefaCreateDto dto)
        {
            var created = await _service.CreateAsync(dto.ToEntity());
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created.ToResponse());
        }

        [HttpPut("{id: guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] TarefaUpdate dto)
        {
            var ok = await _service.UpdateAsync(id, tarefa => tarefa.ApplyUpdate(dto));
            if (!ok) return NotFound(new { message = "Tarefa não encontrada." });
            
            return NoContent();
        }

        [HttpDelete("{id: guid}")]
        public async Task<IActionResult> Delete(Guid id)
        { 
            var tarefa = await _service.DeleteAsync(id);
            if (!tarefa) return NotFound(new { message = "Tarefa não encontrada." });

            return NoContent();
        }
    }
}
