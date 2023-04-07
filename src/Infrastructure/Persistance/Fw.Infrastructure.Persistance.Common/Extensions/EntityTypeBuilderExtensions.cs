using Fw.Domain.Common.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fw.Infrastructure.Persistance.Common.Extensions;

public static class EntityTypeBuilderExtensions
{
    public static EntityTypeBuilder<TEntity> ConfigureBaseEntity<TEntity>(this EntityTypeBuilder<TEntity> builder)
        where TEntity : class, IEntity
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.ClientId).IsRequired();
        builder.Property(e => e.IsActive).IsRequired();
        builder.Property(e => e.IsDeleted).IsRequired();
        builder.Property(e => e.Version);

        return builder;
    }

    public static EntityTypeBuilder<TEntity> ConfigureAuditableEntity<TEntity>(this EntityTypeBuilder<TEntity> builder)
        where TEntity : class, IAuditable
    {
        builder.Property(e => e.CreatedBy).IsRequired();
        builder.Property(e => e.CreatedDate).IsRequired();
        builder.Property(e => e.LastModifiedBy).IsRequired(false);
        builder.Property(e => e.LastModifiedDate).IsRequired(false);

        return builder;
    }
}
