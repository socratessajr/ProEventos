using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Context;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Persistence
{
    public class EventoPersist : GeralPersist, IEventoPersist
    {
        private readonly ProEventosContext _context;
        public EventoPersist(ProEventosContext context) : base(context)
        {
            this._context = context;
            //context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        
        private IQueryable<Evento> GetEventosQuery(bool includePalestrantes)
        {
            IQueryable<Evento> query = _context.Eventos.Include(e => e.Lotes).Include(e => e.RedesSociais);

            if(includePalestrantes){
                query = query.Include(e => e.PalestrantesEventos).ThenInclude(pe => pe.Palestrante);
            }
            return query.AsNoTracking();
        }

        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            var query = GetEventosQuery(includePalestrantes);

            query = query.OrderBy(e => e.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrantes = false)
        {
            var query = GetEventosQuery(includePalestrantes);

            query = query.Where(e => e.Id == eventoId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
            var query = GetEventosQuery(includePalestrantes);

            query = query.OrderBy(e => e.Id).Where(e => e.Tema.ToLower().Contains(tema.ToLower()));

            return await query.ToArrayAsync();
        }
    }
}