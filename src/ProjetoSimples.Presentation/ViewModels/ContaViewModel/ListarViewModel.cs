using System;
using System.ComponentModel.DataAnnotations;

namespace ProjetoSimples.Presentation.ViewModels.ContaViewModel
{
    public class ListarViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Display(Name = "Criado em")]
        public DateTime CriadoEm { get; set; }
    }
}
