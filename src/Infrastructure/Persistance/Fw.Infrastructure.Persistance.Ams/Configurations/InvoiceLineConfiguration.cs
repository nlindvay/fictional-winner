using Fw.Domain.Ams.Entities;
using Fw.Infrastructure.Persistance.Common.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fw.Infrastructure.Persistance.Ams.Configurations;

public class InvoiceLineConfiguration : IEntityTypeConfiguration<InvoiceLine>
{
    public void Configure(EntityTypeBuilder<InvoiceLine> builder)
    {
        builder.ToTable("InvoiceLines");

        builder.ConfigureBaseEntity();
        builder.ConfigureAuditableEntity();

        builder.Property(e => e.LineNumber)
            .IsRequired();

        builder.Property(e => e.LineDescription)
            .IsRequired();

        builder.Property(e => e.LineCost)
            .IsRequired();

        builder.Property(e => e.LineQuantity)
            .IsRequired();

        builder.HasOne(e => e.Invoice);
    }
}