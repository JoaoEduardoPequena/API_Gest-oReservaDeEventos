namespace Domain.Entites
{
    public class Pedido
    {
        public Guid Id_Pedido { get; set; }
        public Guid Id_Evento { get; set; }
        public Evento Evento { get; set; }
        public string NomeCliente { get; set; }
        public string EmailCliente { get; set; }
        public string? DescricaoPedido { get; set; }
        public DateTime DataReserva { get; set; }= DateTime.Now;
        public string UserFaceId { get; set; }
        public DateTime CriadoEm { get; set; } = DateTime.Now;
    }
}
