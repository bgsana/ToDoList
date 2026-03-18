using ToDoList.Models.DTOs;
using ToDoList.Services;
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
            return Ok(items);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var tarefa = await _service.GetByIdAsync(id);

            if (tarefa is null)
                return NotFound(new { message = "Tarefa não encontrada" });

            return Ok(tarefa);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TarefaCreateDto dto)
        {
            var created = await _service.CreateAsync(dto);

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, TarefaUpdate dto)
        {
            var ok = await _service.UpdateAsync(id, tarefa => tarefa.ApplyUpdate(dto));

            return ok ? NoContent() : NotFound();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var tarefa = await _service.DeleteAsync(id);

            if (!tarefa)
                return NotFound(new { message = "Tarefa não encontrada." });

            return NoContent();
        }
    }
}