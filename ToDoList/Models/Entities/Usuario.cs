using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models.Entities;
public class Usuario
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "O nome do Usuário é obrigatório")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "O Email do Usuário é obrigatório")]
    public string Email { get; set; }

    public ICollection<Tarefa> Tarefas { get; set; } = new List<Tarefa>();
}
