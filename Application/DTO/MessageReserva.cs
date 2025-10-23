using Domain.Entites;

namespace Application.DTO
{
    public class MessageReserva
    {
        public Guid Id_Evento { get; set; }
        public string NomeCliente { get; set; }
        public string EmailCliente { get; set; }
        public DateTime DataReserva { get; set; } = DateTime.Now;
        public string UserFaceId { get; set; }
    }
}
