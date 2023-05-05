using Fw.Infrastructure.Persistance.Common.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fw.Infrastructure.Persistance.Common.Configurations;

public class AuditHistoryConfiguration : IEntityTypeConfiguration<AuditHistory>

{
    public void Configure(EntityTypeBuilder<AuditHistory> builder)
    {
        builder.ToTable("AuditHistory", "Audit").Ignore(t => t.NewValues).Ignore(t => t.OldValues);

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Username);
        builder.Property(e => e.EntityId);
        builder.Property(e => e.EntityType);
        builder.Property(e => e.Kind);
        
        builder.Property(e => e.Created).HasDefaultValue(DateTimeOffset.Now);
        builder.Property(e => e.Changed).HasMaxLength(2048);

    }
}