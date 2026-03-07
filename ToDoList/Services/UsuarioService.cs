using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDoList.Data;
using ToDoList.Models.DTOs;

namespace ToDoList.Services;

public class UsuarioService(AppDbContext context)
{
    public async Task<List<UsuarioResponseDto>> GetAllAsync() // Pega todos os usuários
    {
        var usuarios = await context.Usuarios
            .Include(u => u.Tarefas)
            .AsNoTracking()
            .ToListAsync();

        return usuarios.Select(u => u.ToResponse()).ToList();
    }

    public async Task<UsuarioResponseDto> GetByIdAsync(Guid Id) // Pega um usuário específico
    {
        var usuario = await context.Usuarios
            .Include(u => u.Tarefas)
            .FirstOrDefaultAsync(u => u.Id == Id);

        return usuario.ToResponse();
    }

    public async Task<UsuarioResponseDto> CreateAsync(UsuarioCreateDto dto) // Cria um usuário
    {
        var usuario = dto.ToEntity();
        context.Usuarios.Add(usuario);
        await context.SaveChangesAsync();

        return usuario.ToResponse();
    }
}