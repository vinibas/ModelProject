using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Vini.ModelProject.Application.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O nome é obrigatório")]
        public string Nome { get; set; }

        [Display(Name = "Senha")]
        [Required(ErrorMessage = "A senha é obrigatória")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
    }
}
