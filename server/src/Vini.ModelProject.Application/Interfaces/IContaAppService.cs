using System.Collections.Generic;
using System.Threading.Tasks;
using Vini.ModelProject.Application.ViewModels;

namespace Vini.ModelProject.Application.Interfaces
{
    public interface IContaAppService
    {
        Task<IList<string>> CadastrarUsuário(CadastrarUsuárioViewModel vm);
        Task<IEnumerable<ListarViewModel>> Listar();
        Task<IList<string>> Login(LoginViewModel vm);
        void Logout();
    }
}