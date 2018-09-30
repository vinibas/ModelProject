using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Vini.ModelProject.Domain.Interfaces.Services
{
    public interface IUsuárioService
    {
        Task<IEnumerable<Usuário>> ListarTodosAsync();
        Task AdicionarAsync(Usuário usuário);
    }
}
