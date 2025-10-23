using Domain.Entites;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;


namespace Persistence.Mappings
{
    public class EventoMap : IEntityTypeConfiguration<Evento>
    {
        public void Configure(EntityTypeBuilder<Evento> builder)
        {
            builder.ToTable("Evento");

            builder.HasKey(c => c.Id_Evento);

            builder.Property(c => c.Nome)
            .HasColumnName("Nome")
            .HasColumnType("varchar(400)");

            builder.Property(c => c.DataEvento)
            .HasColumnName("DataEvento")
            .HasColumnType("datetime");
        }
    }
}
