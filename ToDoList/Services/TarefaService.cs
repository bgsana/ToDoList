using ToDoList.Data;
using ToDoList.Models.Entities;
using Microsoft.EntityFrameworkCore;
using ToDoList.Models.DTOs;

namespace ToDoList.Services
{
    public class TarefaService
    {
        private readonly AppDbContext _context;

        // método construtor
        public TarefaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<TarefaResponse>> GetAllAsync(bool? concluida = null)
        {
            // variável que vai consultar as tarefas
            var query = _context.Tarefas.AsQueryable();

            if (concluida is not null)
            {
                query = query.Where(t => t.Concluida == concluida);
            }

            var tarefas = await query.AsNoTracking().ToListAsync();

            return tarefas.Select(t => t.ToResponse()).ToList();
        }

        public async Task<TarefaResponse> GetByIdAsync(Guid id)
        {
            var tarefa = await _context.Tarefas.FindAsync(id);
            return tarefa.ToResponse();
        }

        public async Task<TarefaResponse> CreateAsync(TarefaCreateDto dto)
        {
            var tarefa = dto.ToEntity();

            _context.Tarefas.Add(tarefa);
            await _context.SaveChangesAsync();

            return tarefa.ToResponse();
        }

        public async Task<bool> UpdateAsync(Guid id, Action<Tarefa> updateAction)
        {
            var tarefa = await _context.Tarefas.FindAsync(id);
            if (tarefa is null) return false;

            updateAction(tarefa);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var tarefa = await _context.Tarefas.FindAsync(id);
            if (tarefa is null) return false;

            _context.Tarefas.Remove(tarefa);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
