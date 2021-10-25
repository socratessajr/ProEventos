using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProEventos.Persistence;
using ProEventos.Domain;
using ProEventos.Persistence.Context;
using ProEventos.Application.Contratos;
using Microsoft.AspNetCore.Http;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventoController : ControllerBase
    {
        private readonly IEventoService _eventoService;
        public EventoController(IEventoService eventoService)
        {
            this._eventoService = eventoService;
        }

        //Pode usar o retorno da classe normalemente, apenas um exemplo de uso
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var eventos = await _eventoService.GetAllEventosAsync(true);
                if (eventos == null) return NotFound("Nenhum evento encontrado!");

                return Ok(eventos);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                $"Erro ao tentar recuprar lista de eventos. Erro: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Evento>> GetById(int id)
        {
            try
            {
                var evento = await _eventoService.GetEventoByIdAsync(id, true);
                if (evento == null) return NotFound("Evento encontrado!");

                return Ok(evento);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                $"Erro ao tentar recuprar evento. Erro: {ex.Message}");
            }
        }

        //OU: [HttpGet("tema/{tema}")]
        [HttpGet("{tema}/tema")]
        public async Task<ActionResult<Evento>> GetByTema(string tema)
        {
            try
            {
                var eventos = await _eventoService.GetAllEventosByTemaAsync(tema, true);
                if (eventos == null) return NotFound("Eventos por tema não encontrados!");

                return Ok(eventos);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                $"Erro ao tentar recuprar eventos. Erro: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Evento model)
        {
            try
            {
                var evento = await _eventoService.AddEvento(model);
                if (evento == null) return BadRequest("Erro ao tentar adicionar envento");

                return Ok(evento);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                $"Erro ao tentar adicionar evento. Erro: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Evento model)
        {
            try
            {
                var evento = await _eventoService.UpdateEvento(id, model);
                if (evento == null) return BadRequest("Erro ao tentar atualizar envento");

                return Ok(evento);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                $"Erro ao tentar atualizar evento. Erro: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                return await _eventoService.DeleteEvento(id)
                    ? Ok(true)
                    : BadRequest("Erro ao tentar excluir envento");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                $"Erro ao tentar excluir evento. Erro: {ex.Message}");
            }
        }
    }
}
