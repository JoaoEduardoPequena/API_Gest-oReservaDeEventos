using Domain.Entites;

namespace Domain.Interfaces
{
    public interface IRepoPedido
    {
        public Task<bool> CriarPedido(Pedido pedido);
    }
}
