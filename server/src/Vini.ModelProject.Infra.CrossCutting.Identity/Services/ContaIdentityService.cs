using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vini.ModelProject.Infra.CrossCutting.Identity.Models;

namespace Vini.ModelProject.Infra.CrossCutting.Identity.Services
{
    public class ContaIdentityService : IContaIdentityService
    {
        private readonly UserManager<UsuárioIdentity> _userManager;
        private readonly SignInManager<UsuárioIdentity> _signInManager;

        public ContaIdentityService(
            UserManager<UsuárioIdentity> userManager,
            SignInManager<UsuárioIdentity> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IList<string>> CreateAsync(UsuárioIdentity usuário, string senha)
        {
            var resultado = await _userManager.CreateAsync(usuário, senha);

            if (resultado.Succeeded)
                return new List<string>();
            else
            {
                var erros = new List<string>();

                foreach (var error in resultado.Errors)
                    switch (error.Code)
                    {
                        case "DuplicateUserName":
                            erros.Add("O nome de usuário escolhido não está disponível. Escolha outro.");
                            break;
                        case "DuplicateEmail":
                            erros.Add("O e-mail escolhido já possui um cadastro.");
                            break;
                        default:
                            erros.Add(error.Description);
                            break;
                    }

                return erros;
            }
        }

        public async Task SignInAsync(UsuárioIdentity usuário)
            => await _signInManager.SignInAsync(usuário, false);

        public async Task<IList<string>> PasswordSignInAsync(string nomeUsuário, string senha)
        {
            if ((await _signInManager.PasswordSignInAsync(nomeUsuário, senha, false, false)).Succeeded)
                return new List<string>();
            else
                return new List<string> { "Falha: Usuário ou senha incorretos!" };
        }

        public async Task SignOutAsync()
            => await _signInManager.SignOutAsync();

        public async Task<UsuárioIdentity> FindByNameAsync(string nomeUsuário)
            => await _userManager.FindByNameAsync(nomeUsuário);
    }
}
