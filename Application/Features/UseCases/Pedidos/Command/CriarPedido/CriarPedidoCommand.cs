using MediatR;

namespace Application.Features.UseCases.Pedidos.Command.CriarPedido
{
    public record CriarPedidoCommand
    (
        Guid Id_Evento,
        string UserFaceId,
        string NomeCliente,
        string EmailCliente
    ) :IRequest<bool>;
}
