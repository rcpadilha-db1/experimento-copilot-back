using Experimento.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Experimento.Data.Mappings;

public class RideMapping : IEntityTypeConfiguration<Ride>
{
    public void Configure(EntityTypeBuilder<Ride> builder)
    {
        builder.ToTable("Ride");

        builder
            .HasKey(x => x.Id)
            .HasName("PK_RIDE")
            .IsClustered();

        builder
            .Property(x => x.Id)
            .HasColumnName("Id")
            .HasColumnType("varchar")
            .HasMaxLength(10)
            .IsRequired();

        builder
            .Property(x => x.Date)
            .HasColumnName("Date")
            .HasColumnType("datetime")
            .IsRequired();

        builder
            .HasOne(x => x.Vehicle)
            .WithMany()
            .HasForeignKey(x => x.VehicleId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder
            .HasOne(x => x.Rider)
            .WithMany()
            .HasForeignKey(x => x.RiderId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}