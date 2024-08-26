using Domain.Caronas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mapping;

public class CaronaMapping : IEntityTypeConfiguration<Carona>
{
    public void Configure(EntityTypeBuilder<Carona> builder)
    {
        builder.ToTable(nameof(Carona));

        builder.Property(x => x.Id)
            .HasColumnName(nameof(Carona.Id))
            .HasMaxLength(50)
            .ValueGeneratedNever();

        builder.Property(x => x.UsuarioId)
            .HasColumnName(nameof(Carona.UsuarioId))
            .IsRequired();
        
        builder.Property(x => x.VeiculoId)
            .HasColumnName(nameof(Carona.VeiculoId))
            .IsRequired();
        
        builder.Property(x => x.Data)
            .HasColumnName(nameof(Carona.Data))
            .IsRequired();
        
        builder.HasOne(x => x.Usuario)
            .WithMany(u => u.Caronas)
            .HasForeignKey(x => x.UsuarioId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Veiculo)
            .WithMany(v => v.Caronas)
            .HasForeignKey(x => x.VeiculoId);
    }
}