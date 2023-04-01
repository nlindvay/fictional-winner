using Fw.Domain.Wms.Entities;
using Fw.Infrastructure.Persistance.Common.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fw.Infrastructure.Persistance.Wms.Configurations;

public class ReceiveLineConfiguration : IEntityTypeConfiguration<ReceiveLine>
{
    public void Configure(EntityTypeBuilder<ReceiveLine> builder)
    {
        builder.ToTable("ReceiveLines");

        builder.ConfigureBaseEntity();
        builder.ConfigureAuditableEntity();
        
        builder.Property(x => x.LineNumber)
            .IsRequired();

        builder.Property(x => x.Quantity)
            .IsRequired();

        builder.Property(x => x.SkuId)
            .IsRequired();

        builder.Property(x => x.ReceiveId)
            .IsRequired();

        builder.HasOne(x => x.Sku)
            .WithMany()
            .HasForeignKey(x => x.SkuId);

        builder.HasOne(x => x.Receive)
            .WithMany(x => x.ReceiveLines)
            .HasForeignKey(x => x.ReceiveId);
    }
}