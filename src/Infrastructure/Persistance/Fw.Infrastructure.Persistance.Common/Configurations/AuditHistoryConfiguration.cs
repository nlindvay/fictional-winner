using System.Collections.Immutable;
using System.Text.Json;
using Fw.Infrastructure.Persistance.Common.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Fw.Infrastructure.Persistance.Common.Configurations;

public class AuditHistoryConfiguration : IEntityTypeConfiguration<AuditHistory>

{
    public void Configure(EntityTypeBuilder<AuditHistory> builder)
    {
        var converter = new ValueConverter<Dictionary<string, object>, string>(
            to => JsonSerializer.Serialize(to, (JsonSerializerOptions)null),
            from => JsonSerializer.Deserialize<Dictionary<string, object>>(from, (JsonSerializerOptions)null)
        );

        builder.ToTable("AuditHistory");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.UserId);
        builder.Property(e => e.EntityId);
        builder.Property(e => e.EntityType);
        builder.Property(e => e.RootId);
        builder.Property(e => e.RootType);
        builder.Property(e => e.Action);
        builder.Property(e => e.Timestamp).HasDefaultValueSql("GETUTCDATE()");
        builder.Property(e => e.OldValues).HasColumnType("nvarchar(max)").HasConversion(converter);
        builder.Property(e => e.NewValues).HasColumnType("nvarchar(max)").HasConversion(converter);
    }
}