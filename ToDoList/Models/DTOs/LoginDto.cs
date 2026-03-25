using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models.DTOs
{
    public class LoginDto
    {
        [Required(ErrorMessage = "O e-mail é obrigatório!")]
        [EmailAddress(ErrorMessage = "O e-mail tem formato inválido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória!")]
        public string Senha { get; set; }
    }
}
