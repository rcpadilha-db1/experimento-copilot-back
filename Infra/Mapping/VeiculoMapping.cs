using Domain.Veiculos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mapping;

public class VeiculoMapping : IEntityTypeConfiguration<Veiculo>
{
    public void Configure(EntityTypeBuilder<Veiculo> builder)
    {
        builder.Property(x => x.Id)
            .HasColumnName(nameof(Veiculo.Id))
            .HasMaxLength(50)
            .ValueGeneratedNever();
        
        builder.Property(x => x.Placa)
            .HasColumnName(nameof(Veiculo.Placa))
            .HasMaxLength(8)
            .IsRequired();
        
        builder.Property(x => x.Capacidade)
            .HasColumnName(nameof(Veiculo.Capacidade))
            .IsRequired();
        
        builder.Property(x => x.UsuarioId)
            .HasColumnName(nameof(Veiculo.UsuarioId))
            .IsRequired();
        
        builder.HasOne(x => x.Usuario)
            .WithMany(u => u.Veiculos)
            .HasForeignKey(x => x.UsuarioId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}