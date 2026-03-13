using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoList.Models.Entities
{
    public class Comentario
    {
        public Guid Id { get; set; }

        [MaxLength(100)]
        public string Texto { get; set; }

        public DateTime Data { get; set; }

        public Guid TarefaId { get; set; }
        public Tarefa? Tarefa { get; set; }

        public Guid UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
    }
}
