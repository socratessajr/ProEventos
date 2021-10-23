using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProEventos.WebAPI.Models;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventoController : ControllerBase
    {
        private readonly ILogger<EventoController> _logger;

        public EventoController(ILogger<EventoController> logger)
        {
        }

        [HttpGet]
        public IEnumerable<Evento> Get()
        {
            return new Evento[] {
                new Evento
                {
                    EventoId = 1,
                    Tema = "Angular 11",
                    Local = "BH",
                    DataEvento = "25/10/2021",
                    QtdPessoas = 250,
                    Lote = "1º Lote",
                    ImagemURL = "foto1.jpg"
                },
                new Evento
                {
                    EventoId = 2,
                    Tema = "Angular 11 e .NET Core 5",
                    Local = "SP",
                    DataEvento = "25/10/2021",
                    QtdPessoas = 250,
                    Lote = "1º Lote",
                    ImagemURL = "foto2.jpg"
                }
            };
        }

        [HttpGet("{id}")]
        public Evento GetById(int id)
        {
            return new Evento[] {
                new Evento
                {
                    EventoId = 1,
                    Tema = "Angular 11",
                    Local = "BH",
                    DataEvento = "25/10/2021",
                    QtdPessoas = 250,
                    Lote = "1º Lote",
                    ImagemURL = "foto1.jpg"
                },
                new Evento
                {
                    EventoId = 2,
                    Tema = "Angular 11 e .NET Core 5",
                    Local = "SP",
                    DataEvento = "25/10/2021",
                    QtdPessoas = 250,
                    Lote = "1º Lote",
                    ImagemURL = "foto2.jpg"
                }
            }.Where(e => e.EventoId == id).FirstOrDefault();
        }

        [HttpPost]
        public string Post()
        {
            return "Exemplo de Post";
        }

        [HttpPut("{id}")]
        public string Put(int id)
        {
            return $"Exemplo de Put {id}";
        }

        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            return $"Exemplo de Delete {id}";
        }
    }
}
