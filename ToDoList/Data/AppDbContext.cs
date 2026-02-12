using ToDoList.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ToDoList.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options)
    : base(options) { }

    //Informar a model para o nosso contexto e como ela irá ser criada ao executar as migrations
    public DbSet<Tarefa> Tarefas { get; set; }
}


