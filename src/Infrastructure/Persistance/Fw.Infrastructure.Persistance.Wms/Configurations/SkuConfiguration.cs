using Fw.Domain.Wms.Entities;
using Fw.Infrastructure.Persistance.Wms.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fw.Infrastructure.Persistance.Wms.Configurations;

public class SkuConfiguration : IEntityTypeConfiguration<Sku>
{
    public void Configure(EntityTypeBuilder<Sku> builder)
    {
        builder.ToTable("Skus");

        builder.ConfigureBaseEntity();
        builder.ConfigureAuditableEntity();

        builder.Property(e => e.SkuCode)
            .HasColumnType("nvarchar(50)")
            .IsRequired();

        builder.Property(e => e.SkuDescription)
            .HasColumnType("nvarchar(50)");
    }
}