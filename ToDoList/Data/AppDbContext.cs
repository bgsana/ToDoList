using ToDoList.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ToDoList.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Tarefa> Tarefas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Usuario → Tarefas
            modelBuilder.Entity<Tarefa>()
                .HasOne(t => t.Usuario)
                .WithMany(u => u.Tarefas)
                .HasForeignKey(t => t.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            // Comentario → Tarefa
            modelBuilder.Entity<Comentario>()
                .HasOne(c => c.Tarefa)
                .WithMany(t => t.Comentarios)
                .HasForeignKey(c => c.TarefaId)
                .OnDelete(DeleteBehavior.Cascade);

            // Comentario → Usuario
            modelBuilder.Entity<Comentario>()
                .HasOne(c => c.Usuario)
                .WithMany()
                .HasForeignKey(c => c.UsuarioId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Email)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}