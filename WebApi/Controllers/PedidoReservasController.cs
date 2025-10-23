using Application.Features.UseCases.Pedidos.Command.CriarPedido;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Tags("Reservas")]
    [Route("api/reserva")]
    [ApiExplorerSettings(GroupName = "reservas")]
    [ApiController]
    public class PedidoReservasController : Controller
    {
        private readonly ISender _sender;
        public PedidoReservasController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        [ProducesResponseType(typeof(bool), StatusCodes.Status201Created)]
        public async Task<IActionResult> CriarPedidos(CriarPedidoCommand request)
        {
            return Ok(await _sender.Send(request));
        }
    }
}
