using Experimento.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Experimento.Data.Mappings;

public class VehicleMapping : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.ToTable("Vehicle");

        builder
            .HasKey(x => x.Id)
            .HasName("PK_VEHICLE")
            .IsClustered();

        builder
            .Property(x => x.Id)
            .HasColumnName("Id")
            .HasColumnType("varchar")
            .HasMaxLength(10)
            .IsRequired();

        builder
            .Property(x => x.Plate)
            .HasColumnName("Plate")
            .HasColumnType("varchar")
            .HasMaxLength(20)
            .IsRequired();

        builder
            .Property(x => x.Capacity)
            .HasColumnName("Capacity")
            .HasMaxLength(10)
            .IsRequired();

        builder
            .HasOne(x => x.Owner)
            .WithMany()
            .HasForeignKey(x => x.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}