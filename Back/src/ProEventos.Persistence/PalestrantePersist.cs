using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Context;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Persistence
{
    public class PalestrantePersist : GeralPersist, IPalestrantePersist
    {
        private readonly ProEventosContext _context;
        public PalestrantePersist(ProEventosContext context): base(context)
        {
            this._context = context;

        }
        private IQueryable<Palestrante> GetPalestranteQuery(bool includeEventos){
            IQueryable<Palestrante> query = _context.Palestrantes.Include(e => e.RedesSociais);

            if(includeEventos){
                query = query.Include(e => e.PalestrantesEventos).ThenInclude(pe => pe.Evento);
            }

            return query.AsNoTracking();
        }

        public async Task<Palestrante[]> GetAllPalestrantesAsync(bool includeEventos = false)
        {
            var query = GetPalestranteQuery(includeEventos);
            return await query.OrderBy(p => p.Id).ToArrayAsync();
        }

        public async Task<Palestrante> GetPalestranteByIdAsync(int palestranteId, bool includeEventos = false)
        {
            var query = GetPalestranteQuery(includeEventos);
            return await query.FirstOrDefaultAsync(p => p.Id == palestranteId);
        }

        public async Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string nome, bool includeEventos = false)
        {
            var query = GetPalestranteQuery(includeEventos);
            return await query.OrderBy(p => p.Id).Where(p => p.Nome.ToLower().Contains(nome.ToLower())).ToArrayAsync();
        }
    }
}