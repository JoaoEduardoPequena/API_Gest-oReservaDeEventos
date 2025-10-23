using Domain.Entites;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Mappings
{
    public class PedidoMap : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.ToTable("Pedido");

            builder.HasKey(p => p.Id_Pedido);

            builder.Property(p => p.Id_Pedido)
                .IsRequired()
                .HasColumnName("Id_Produto")
                .HasColumnType("Guid")
                .ValueGeneratedOnAdd();

            builder.Property(p => p.DescricaoPedido)
                .HasColumnType("varchar(400)");

            builder.Property(p => p.EmailCliente)
             .HasColumnType("varchar(400)");

            builder.Property(p => p.NomeCliente)
            .HasColumnType("varchar(400)");


            builder.Property(p => p.DataReserva)
            .HasColumnType("datetime");

            // Configurando o relacionamento um-para-muito entre Pedido e Evento
            builder.HasOne(p => p.Evento)           // Uma Evento
                .WithMany(p => p.Pedido)               // tem muitos Pedido
                .HasForeignKey(p => p.Id_Evento); // Chave estrangeira
        }
    }
}
