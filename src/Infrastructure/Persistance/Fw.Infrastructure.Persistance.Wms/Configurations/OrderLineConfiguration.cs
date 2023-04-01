using Fw.Domain.Wms.Entities;
using Fw.Infrastructure.Persistance.Wms.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fw.Infrastructure.Persistance.Wms.Configurations;

public class OrderLineConfiguration : IEntityTypeConfiguration<OrderLine>
{
    public void Configure(EntityTypeBuilder<OrderLine> builder)
    {
        builder.ToTable("OrderLine");

        builder.ConfigureBaseEntity();
        builder.ConfigureAuditableEntity();

        builder.Property(x => x.LineNumber)
            .IsRequired();

        builder.Property(x => x.Quantity)
            .IsRequired();

        builder.Property(x => x.SkuId)
            .IsRequired();

        builder.HasOne(x => x.Sku)
            .WithMany()
            .HasForeignKey(x => x.SkuId);

        builder.Property(x => x.OrderId)
            .IsRequired();

        builder
            .HasOne(x => x.Order)
            .WithMany(x => x.OrderLines)
            .HasForeignKey(x => x.OrderId);
    }
}