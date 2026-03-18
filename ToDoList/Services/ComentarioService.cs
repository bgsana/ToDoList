using Microsoft.EntityFrameworkCore;
using ToDoList.Data;
using ToDoList.Models.DTOs;
using ToDoList.Models.Entities;

namespace ToDoList.Services
{
    public class ComentarioService
    {
        private readonly AppDbContext _context;

        public ComentarioService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ComentarioResponseDto> AdicionarComentario(Guid tarefaId, ComentarioCreateDto dto)
        {
            var comentario = new Comentario
            {
                Id = Guid.NewGuid(),
                Texto = dto.Texto,
                Data = DateTime.UtcNow,
                TarefaId = tarefaId,
                UsuarioId = dto.UsuarioId
            };

            _context.Comentarios.Add(comentario);
            await _context.SaveChangesAsync();

            var usuario = await _context.Usuarios.FindAsync(dto.UsuarioId);

            return new ComentarioResponseDto
            {
                Texto = comentario.Texto,
                DataCriacao = comentario.Data,
                NomeUsuario = usuario.Nome
            };
        }

        public async Task<List<ComentarioResponseDto>> ListarComentarios(Guid tarefaId)
        {
            return await _context.Comentarios
                .Where(c => c.TarefaId == tarefaId)
                .Include(c => c.Usuario)
                .Select(c => new ComentarioResponseDto
                {
                    Texto = c.Texto,
                    DataCriacao = c.Data,
                    NomeUsuario = c.Usuario.Nome
                })
                .ToListAsync();
        }
    }
}