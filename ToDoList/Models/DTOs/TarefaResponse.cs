namespace ToDoList.Models.DTOs
{
    public class TarefaResponse
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; } = "";
        public string? Descricao { get; set; }
        public bool Concluida { get; set; }
        public DateTime CriadaEm { get; set; }
        public DateTime AtualizadaEm { get; set; }
    }
}
