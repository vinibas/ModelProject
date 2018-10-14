using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vini.ModelProject.Domain.Interfaces.Repositories;
using Vini.ModelProject.Domain.Interfaces.Services;

namespace Vini.ModelProject.Domain.Services
{
    public class UsuárioService : IUsuárioService
    {
        private readonly IUsuárioRepository _usuárioRepository;

        public UsuárioService(IUsuárioRepository usuárioRepository)
        {
            this._usuárioRepository = usuárioRepository;
        }

        public async Task AdicionarAsync(Usuário usuário)
        {
            //var erros = new List<string>();

            await _usuárioRepository.AdicionarAsync(usuário);
            
            //return erros;
        }

        public async Task<IEnumerable<Usuário>> ListarTodosAsync()
        {
            return await _usuárioRepository.ListarTodosAsync();
        }

        public async Task<Usuário> ObterPorIdAsync(Guid id)
            => await _usuárioRepository.ObterPorIdAsync(id);
    }
}
