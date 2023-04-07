using Fw.Domain.Common.Enums;
using Fw.Domain.Tms.Entities;
using Fw.Infrastructure.Persistance.Common.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fw.Infrastructure.Persistance.Tms.Configurations;

public class ShipmentConfiguration : IEntityTypeConfiguration<Shipment>

{
    public void Configure(EntityTypeBuilder<Shipment> builder)
    {
        builder.ToTable("Shipments");

        builder.ConfigureBaseEntity();
        builder.ConfigureAuditableEntity();

        builder.Property(e => e.PrimaryReference)
            .HasColumnType("nvarchar(50)")
            .IsRequired();

        builder.Property(e => e.SecondaryReference)
            .HasColumnType("nvarchar(50)")
            .IsRequired();

        builder.Property(e => e.ShipmentStatus)
            .IsRequired();

        builder.HasMany(e => e.Packages);

        builder.Property(e => e.OrderId)
            .IsRequired(false);

        builder.Property(e => e.OrderStatus)
            .HasDefaultValue(OrderStatus.None);

        builder.Property(e => e.InvoiceId)
            .IsRequired(false);

        builder.Property(e => e.InvoiceStatus)
            .HasDefaultValue(InvoiceStatus.None);
    }
}