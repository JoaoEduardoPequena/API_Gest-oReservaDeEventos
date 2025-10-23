namespace Domain.Entites
{
    public class Produto
    {
        public int Id_Produto { get; set; }
        public string Nome { get; set; }
        public DateTime DataCriacao { get; set; }
        public int Id_Categoria { get; set; }
        public Evento Categoria { get; set; }
    }
}
