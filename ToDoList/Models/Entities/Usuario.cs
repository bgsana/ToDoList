using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models.Entities
{
    public class Usuario
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "O Nome do Usuárió é obrigatório!")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "O Email do Usuárió é obrigatório!")]
    public string Email { get; set; }

    public string PasswordHash { get; set; } = string.Empty;

    public ICollection<Tarefa> Tarefas { get; set; } = new List<Tarefa>();
}
}
