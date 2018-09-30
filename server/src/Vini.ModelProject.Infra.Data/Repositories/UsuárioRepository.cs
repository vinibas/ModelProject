using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vini.ModelProject.Domain;
using Vini.ModelProject.Domain.Interfaces.Repositories;

namespace Vini.ModelProject.Infra.Data.Repositories
{
    public class UsuárioRepository : IDisposable, IUsuárioRepository
    {
        private readonly ModelProjectContext _context;

        public UsuárioRepository(ModelProjectContext applicationDbContext)
            => _context = applicationDbContext;

        public async Task<Usuário> ObterPorIdAsync(Guid id)
            => await _context.Usuário.FindAsync(id);

        public async Task<int> AdicionarAsync(Usuário usuário)
        {
            await _context.Usuário.AddAsync(usuário);
            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Usuário>> ListarTodosAsync()
            => await _context.Usuário.ToListAsync();

        public void Dispose()
            => _context.Dispose();
    }
}
