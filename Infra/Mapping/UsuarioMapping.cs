using Domain.Usuarios;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mapping;

public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable(nameof(Usuario));

        builder.Property(x => x.Id)
            .HasColumnName(nameof(Usuario.Id))
            .HasMaxLength(50)
            .ValueGeneratedNever();
        
        builder.Property(x => x.Nome)
            .HasColumnName(nameof(Usuario.Nome))
            .HasMaxLength(150)
            .IsRequired();
        
        builder.Property(x => x.Email)
            .HasColumnName(nameof(Usuario.Email))
            .HasMaxLength(250)
            .IsRequired();
    }
}