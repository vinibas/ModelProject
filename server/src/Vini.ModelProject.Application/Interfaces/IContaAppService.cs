using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vini.ModelProject.Application.ViewModels;

namespace Vini.ModelProject.Application.Interfaces
{
    public interface IContaAppService
    {
        Task<IList<string>> CadastrarUsuário(CadastrarUsuárioViewModel vm);
        Task<IEnumerable<ListarViewModel>> Listar();
        Task<IList<string>> LoginAsync(LoginViewModel vm);
        void Logout();
        Task<string> ObterNomeDoUsuárioPorUserNameAsync(string userName);
        Task LogoutAsync();
        Task<IEnumerable<ListarViewModel>> ListarUsuáriosAsync();
    }
}