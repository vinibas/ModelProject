using Microsoft.EntityFrameworkCore;
using ProjetoSimples.Presentation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoSimples.Presentation.Data
{
    public class UsuárioRepository : IDisposable
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private DbSet<Usuário> DbSet;

        public UsuárioRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            DbSet = _applicationDbContext.Set<Usuário>();
        }

        public async Task<Usuário> ObterPorIdAsync(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<int> AdicionarAsync(Usuário usuário)
        {
            await DbSet.AddAsync(usuário);
            return await _applicationDbContext.SaveChangesAsync();
        }
        
        public async Task<IEnumerable<Usuário>> ListarTodosAsync()
        {
            return await DbSet.ToListAsync();
        }

        public void Dispose()
            => _applicationDbContext.Dispose();

    }
}
