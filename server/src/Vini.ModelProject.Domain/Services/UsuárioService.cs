using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vini.ModelProject.Domain.Interfaces.Services;

namespace Vini.ModelProject.Domain.Services
{
    public class UsuárioService : IUsuárioService
    {
        private readonly UsuárioService _usuárioService;

        public UsuárioService(UsuárioService usuárioService)
        {
            this._usuárioService = usuárioService;
        }

        public async Task AdicionarAsync(Usuário usuário)
        {
            //var erros = new List<string>();

            await _usuárioService.AdicionarAsync(usuário);
            
            //return erros;
        }

        public async Task<IEnumerable<Usuário>> ListarTodosAsync()
        {
            return await _usuárioService.ListarTodosAsync();
        }
    }
}
