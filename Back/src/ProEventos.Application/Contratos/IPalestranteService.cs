using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Application.Contratos
{
    public interface IPalestranteService
    {
        Task<Palestrante> AddPalestrante(Palestrante model);
        Task<Palestrante> UpdatePalestrante(int eventoId, Palestrante model);
        Task<bool> DeletePalestrante(int palestranteId);

        Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string tema, bool includeEventos = false);
        Task<Palestrante[]> GetAllPalestrantesAsync(bool includeEventos = false);
        Task<Palestrante> GetPalestranteByIdAsync(int eventoId, bool includeEventos = false);
    }
}