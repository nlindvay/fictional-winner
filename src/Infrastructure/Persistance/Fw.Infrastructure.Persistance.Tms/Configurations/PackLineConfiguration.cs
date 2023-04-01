using Fw.Domain.Tms.Entities;
using Fw.Infrastructure.Persistance.Common.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fw.Infrastructure.Persistance.Tms.Configurations;

public class PackLineConfiguration : IEntityTypeConfiguration<PackLine>
{
    public void Configure(EntityTypeBuilder<PackLine> builder)
    {
        builder.ToTable("PackLines");

        builder.ConfigureBaseEntity();
        builder.ConfigureAuditableEntity();

        builder.Property(e => e.LineNumber)
            .IsRequired();

        builder.Property(e => e.LineQuantity)
            .IsRequired();

        builder.Property(e => e.LineDescription)
            .IsRequired();

        builder.HasOne(e => e.Pack);
    }
}