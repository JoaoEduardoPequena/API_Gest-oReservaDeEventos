
using System.ComponentModel.DataAnnotations;

namespace Domain.Entites
{
    public class Evento
    {
        [Key]
        public int Id_Evento { get; set; }
        public string Nome { get; set; }
        public DateTime DataEvento { get; set; }
        public virtual ICollection<Pedido> Pedido { get; set; }
    }
}
