using System;
using System.Threading.Tasks;
using ProEventos.Application.Contratos;
using ProEventos.Domain;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Application
{
    public class PalestranteService : IPalestranteService
    {
        private readonly IPalestrantePersist _palestranteDB;
        public PalestranteService(IPalestrantePersist palestranteDB)
        {
            this._palestranteDB = palestranteDB;
        }
        public async Task<Palestrante> AddPalestrante(Palestrante model)
        {
            try
            {
                _palestranteDB.Add<Palestrante>(model);
                if (await _palestranteDB.SaveChangesAsync()){
                    return await _palestranteDB.GetPalestranteByIdAsync(model.Id, false);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Palestrante> UpdatePalestrante(int palestranteId, Palestrante model)
        {
            try
            {
                var palestrante = await _palestranteDB.GetPalestranteByIdAsync(palestranteId, false);
                if (palestrante == null) return null;

                model.Id = palestrante.Id;
                
                _palestranteDB.Update<Palestrante>(model);
                if (await _palestranteDB.SaveChangesAsync()){
                    return await _palestranteDB.GetPalestranteByIdAsync(model.Id, false);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeletePalestrante(int palestranteId)
        {
            try
            {
                var palestrante = await _palestranteDB.GetPalestranteByIdAsync(palestranteId, false);
                if (palestrante == null) throw new Exception("Palestrante id " + palestranteId + " não foi encontrado!");

                _palestranteDB.Delete<Palestrante>(palestrante);
                return await _palestranteDB.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Palestrante[]> GetAllPalestrantesAsync(bool includeEventos = false)
        {
            try
            {
                return await _palestranteDB.GetAllPalestrantesAsync(includeEventos);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string tema, bool includeEventos = false)
        {
            try
            {
                return await _palestranteDB.GetAllPalestrantesByNomeAsync(tema, includeEventos);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Palestrante> GetPalestranteByIdAsync(int palestranteId, bool includeEventos = false)
        {
            try
            {
                return await _palestranteDB.GetPalestranteByIdAsync(palestranteId, includeEventos);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}