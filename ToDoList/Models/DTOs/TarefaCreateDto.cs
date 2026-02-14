using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models.DTOs
{
    public class TarefaCreateDto
    {
        [Required, MinLength(3), MaxLength(80)]
        public string Titulo { get; set; } = "";
        [MaxLength(400)]
        public string? Descricao { get; set; }
    }
}
