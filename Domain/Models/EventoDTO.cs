using Domain.Entites;

namespace Domain.Models
{
    public class EventoDTO
    {
        public int Id_Evento { get; set; }
        public string Nome { get; set; }
        public virtual ICollection<Pedido> Pedido { get; set; }
    }
}
