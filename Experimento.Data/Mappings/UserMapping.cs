using Experimento.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Experimento.Data.Mappings;

public class UserMapping : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");

        builder
            .HasKey(x => x.Id)
            .HasName("PK_USER")
            .IsClustered();

        builder
            .Property(x => x.Id)
            .HasColumnName("Id")
            .HasColumnType("varchar")
            .HasMaxLength(10)
            .IsRequired();

        builder
            .Property(x => x.Name)
            .HasColumnName("Name")
            .HasColumnType("varchar")
            .HasMaxLength(20)
            .IsRequired();

        builder
            .Property(x => x.Email)
            .HasColumnName("Email")
            .HasColumnType("varchar")
            .HasMaxLength(100)
            .IsRequired();
    }
}