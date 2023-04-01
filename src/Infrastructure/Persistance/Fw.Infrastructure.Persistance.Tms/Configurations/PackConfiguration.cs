using Fw.Domain.Tms.Entities;
using Fw.Infrastructure.Persistance.Common.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fw.Infrastructure.Persistance.Tms.Configurations;

public class PackConfiguration : IEntityTypeConfiguration<Pack>

{
    public void Configure(EntityTypeBuilder<Pack> builder)
    {
        builder.ToTable("Packs");

        builder.ConfigureBaseEntity();
        builder.ConfigureAuditableEntity();

        builder.HasMany(e => e.PackLines);
    }
}