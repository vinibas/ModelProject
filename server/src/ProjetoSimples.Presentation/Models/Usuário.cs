using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoSimples.Presentation.Models
{
    public class Usuário
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(30)]
        [Column(TypeName = "VARCHAR(30)")]
        public string Nome { get; set; }

        [Required]
        public DateTime CriadoEm { get; set; }
    }
}
