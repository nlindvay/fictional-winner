using Fw.Domain.Ams.Entities;
using Fw.Domain.Common.Enums;
using Fw.Infrastructure.Persistance.Common.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fw.Infrastructure.Persistance.Ams.Configurations;

public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>

{
    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
        builder.ToTable("Invoices");

        builder.ConfigureBaseEntity();
        builder.ConfigureAuditableEntity();

        builder.Property(e => e.PrimaryReference)
            .HasColumnType("nvarchar(50)")
            .IsRequired();

        builder.Property(e => e.SecondaryReference)
            .HasColumnType("nvarchar(50)")
            .IsRequired();

        builder.Property(e => e.InvoiceStatus)
            .IsRequired();

        builder.HasMany(e => e.InvoiceLines);

        builder.Property(e => e.OrderId)
            .IsRequired(false);

        builder.Property(e => e.OrderStatus)
            .HasDefaultValue(OrderStatus.None);

        builder.Property(e => e.ShipmentId)
            .IsRequired(false);

        builder.Property(e => e.ShipmentStatus)
            .HasDefaultValue(ShipmentStatus.None);
    }
}