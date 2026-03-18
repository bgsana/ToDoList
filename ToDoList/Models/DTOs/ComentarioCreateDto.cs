using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models.DTOs
{
    public class ComentarioCreateDto
    {
        [Required]
        [MaxLength(100)]
        public string Texto { get; set; }

        [Required]
        public Guid UsuarioId { get; set; }
    }
}
