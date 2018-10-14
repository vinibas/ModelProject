using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Vini.ModelProject.Application.Interfaces;
using Vini.ModelProject.Application.ViewModels;
using Vini.ModelProject.Domain;
using Vini.ModelProject.Domain.Interfaces.Services;
using Vini.ModelProject.Infra.CrossCutting.Identity.Models;
using Vini.ModelProject.Infra.CrossCutting.Identity.Services;

namespace Vini.ModelProject.Application.AplicationServices
{
    public class ContaAppService : IContaAppService
    {
        private readonly IUsuárioService _usuárioService;
        private readonly IContaIdentityService _contaIdentityService;

        //private readonly IStringLocalizer<ContaController> _localizer;

        public ContaAppService(
            IUsuárioService usuárioRepository,
            IContaIdentityService contaIdentityService)
        //IStringLocalizer<ContaController> localizer)
        {
            _usuárioService = usuárioRepository;
            this._contaIdentityService = contaIdentityService;
            //_localizer = localizer;
        }

        public async Task<IEnumerable<ListarViewModel>> Listar()
        {
            var usuários = await _usuárioService.ListarTodosAsync();
            var listarVM = usuários.Select(u => new ListarViewModel { Id = u.Id, Nome = u.Nome, CriadoEm = u.CriadoEm });

            return listarVM;
        }

        /// <summary>
        /// Cadastra um novo usuário no sistema
        /// </summary>
        /// <param name="vm"></param>
        /// <returns>Uma lista de mensagens de erro, caso não seja possível efetuar o cadastro</returns>
        public async Task<IList<string>> CadastrarUsuário(CadastrarUsuárioViewModel vm)
        {
            var erros = new List<string>();

            var appUser = new UsuárioIdentity() { UserName = vm.Nome };

            var resultadoCriação = await _contaIdentityService.CreateAsync(appUser, vm.Senha);

            if (resultadoCriação.Count == 0)
            {
                var usuário = new Usuário { Id = new Guid(appUser.Id), Nome = vm.Nome, CriadoEm = DateTime.Now };

                await _usuárioService.AdicionarAsync(usuário);
                await _contaIdentityService.SignInAsync(appUser);
            }
            else
                foreach (var erro in resultadoCriação)
                    erros.Add(erro);

            return erros;
        }

        public async Task<IList<string>> LoginAsync(LoginViewModel vm)
            => await _contaIdentityService.PasswordSignInAsync(vm.Nome, vm.Senha);

        public async void Logout()
            => await _contaIdentityService.SignOutAsync();

        public async Task<string> ObterNomeDoUsuárioPorUserNameAsync(string userName)
        {
            var user = await _contaIdentityService.FindByNameAsync(userName);
            return (await this._usuárioService.ObterPorIdAsync(Guid.Parse(user.Id))).Nome;
        }

        public async Task LogoutAsync()
            => await _contaIdentityService.SignOutAsync();

        public async Task<IEnumerable<ListarViewModel>> ListarUsuáriosAsync()
        {
            var usuários = await _usuárioService.ListarTodosAsync();
            return usuários.Select(u => new ListarViewModel { Id = u.Id, Nome = u.Nome, CriadoEm = u.CriadoEm });
        }

        public bool UsuárioEstáLogado(ClaimsPrincipal userClaimsPrincipal)
            => _contaIdentityService.IsUserAuthenticated(userClaimsPrincipal);

        public async Task<string> ObterNomeDoUsuárioLogadoAsync(ClaimsPrincipal userClaimsPrincipal)
        {
            var userId = _contaIdentityService.GetUserId(userClaimsPrincipal);
            return (await _usuárioService.ObterPorIdAsync(new Guid(userId))).Nome;
        }
    }
}
