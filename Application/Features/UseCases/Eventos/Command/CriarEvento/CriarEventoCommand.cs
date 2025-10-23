using MediatR;

namespace Application.Features.UseCases.Pedidos.Command.CriarPedido
{
    public record CriarEventoCommand
    (
        string Nome,
        DateTime DataEvento
    ) :IRequest<bool>;
}
