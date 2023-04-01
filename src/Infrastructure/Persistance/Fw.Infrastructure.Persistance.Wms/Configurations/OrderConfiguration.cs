using Fw.Domain.Common.Interfaces;
using Fw.Domain.Wms.Entities;
using Fw.Infrastructure.Persistance.Wms.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fw.Infrastructure.Persistance.Wms.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");

        builder.ConfigureBaseEntity();
        builder.ConfigureAuditableEntity();

        builder.Property(e => e.CustomerId)
            .IsRequired();

        builder.Property(e => e.PrimaryReference)
            .HasColumnType("nvarchar(50)")
            .IsRequired();

        builder.Property(e => e.SecondaryReference)
            .HasColumnType("nvarchar(50)");

        builder.Property(e => e.OrderStatus)
            .IsRequired();

        builder.HasMany(e => e.OrderLines);
    }
}