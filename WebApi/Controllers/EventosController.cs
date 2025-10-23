using Application.Features.UseCases.Eventos.Queries;
using Application.Features.UseCases.Pedidos.Command.CriarPedido;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Tags("Eventos")]
    [Route("api/evento")]
    [ApiExplorerSettings(GroupName = "eventos")]
    [ApiController]
    public class EventosController : Controller
    {
        private readonly ISender _sender;
        public EventosController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        [ProducesResponseType(typeof(bool), StatusCodes.Status201Created)]
        public async Task<IActionResult> CriarEventos(CriarEventoCommand request)
        {
            return Ok(await _sender.Send(request));
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<EventoDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllEventos()
        {
            return Ok(await _sender.Send(new GetAllEventosQuery()));
        }
    }
}
