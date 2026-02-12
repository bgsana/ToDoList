namespace ToDoList.Models.Entities;

public class Tarefa
{
    public Guid Id { get; set; }
    public string Titulo { get; set; }
    public string? Descricao { get; set; }
    public bool Concluida { get; set; }
    public DateTime CriadaEm { get; set; } = DateTime.UtcNow;
    public DateTime AtualizadaEm { get; set; }
}
