using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vini.ModelProject.Domain;

namespace Vini.ModelProject.Domain.Interfaces.Repositories
{
    public interface IUsuárioRepository
    {
        Task<int> AdicionarAsync(Usuário usuário);
        void Dispose();
        Task<IEnumerable<Usuário>> ListarTodosAsync();
        Task<Usuário> ObterPorIdAsync(Guid id);
    }
}