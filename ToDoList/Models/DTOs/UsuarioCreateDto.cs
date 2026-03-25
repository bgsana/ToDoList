using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models.DTOs
{
    public class UsuarioCreateDto
    {
        [Required, MinLength(3), MaxLength(100)]
        public string Nome { get; set; } = "";

        [Required, EmailAddress]
        public string Email { get; set; } = "";

        [Required, MinLength(8), MaxLength(16)]
        public string Senha { get; set; } = "";
    }
}
