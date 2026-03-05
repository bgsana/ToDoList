using ToDoList.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ToDoList.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options) { }

    //Informar a model para o nosso contexto e como ela irá ser criada ao executar as migrations
    public DbSet<Tarefa> Tarefas { get; set; }

    public DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Relacionamento 1:N
        modelBuilder.Entity<Tarefa>()
            .HasOne<t => t.Usuario> // Uma tarefa tem um usuário
            .WithMany(u => u.Tarefas) // Um usuário tem MUITAS Tarefas
            .hasForeignKey(t => t.UsuarioId) // A Chave estrangeiras
            .onDelete(DeleteBehavior.Cascade); // Se deletar o usuário, as tarefas somem

        base.OnModelCreating(modelBuilder);
    }
}


