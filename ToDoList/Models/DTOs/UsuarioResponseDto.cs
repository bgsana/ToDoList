namespace ToDoList.Models.DTOs
{
    public class UsuarioResponseDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }

        // Lista de tarefas convertidos para o DTO de resposta (evita loop infinito)
        public List<TarefaResponse> Tarefas { get; set; } = [];
    }
}
