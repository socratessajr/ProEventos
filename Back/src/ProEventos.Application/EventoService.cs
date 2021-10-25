using System;
using System.Threading.Tasks;
using ProEventos.Application.Contratos;
using ProEventos.Domain;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Application
{
    public class EventoService : IEventoService
    {
        private readonly IEventoPersist _eventoDB;
        public EventoService(IEventoPersist eventoDB)
        {
            this._eventoDB = eventoDB;
        }
        public async Task<Evento> AddEvento(Evento model)
        {
            try
            {
                _eventoDB.Add<Evento>(model);
                if (await _eventoDB.SaveChangesAsync()){
                    return await _eventoDB.GetEventoByIdAsync(model.Id, false);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Evento> UpdateEvento(int eventoId, Evento model)
        {
            try
            {
                var evento = await _eventoDB.GetEventoByIdAsync(eventoId, false);
                if (evento == null) return null;

                model.Id = evento.Id;
                
                _eventoDB.Update<Evento>(model);
                if (await _eventoDB.SaveChangesAsync()){
                    return await _eventoDB.GetEventoByIdAsync(model.Id, false);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteEvento(int eventoId)
        {
            try
            {
                var evento = await _eventoDB.GetEventoByIdAsync(eventoId, false);
                if (evento == null) throw new Exception("Evento id " + eventoId + " não foi encontrado!");

                _eventoDB.Delete<Evento>(evento);
                return await _eventoDB.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            try
            {
                return await _eventoDB.GetAllEventosAsync(includePalestrantes);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
            try
            {
                return await _eventoDB.GetAllEventosByTemaAsync(tema, includePalestrantes);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrantes = false)
        {
            try
            {
                return await _eventoDB.GetEventoByIdAsync(eventoId, includePalestrantes);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}