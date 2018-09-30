using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Vini.ModelProject.Application.ViewModels
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
