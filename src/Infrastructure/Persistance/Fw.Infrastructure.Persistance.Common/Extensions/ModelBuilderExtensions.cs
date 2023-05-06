using System.Text.Json;
using Fw.Domain.Common.Enums;
using Fw.Infrastructure.Persistance.Common.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Fw.Infrastructure.Persistance.Common.Extensions;

public static class DbContextExtensions
{
    public static void EnsureAudit(this DbContext context, string userId)
    {
        var entries = context.ChangeTracker
            .Entries()
            .Where(e => !AuditUtilities.IsNotAuditable(e.Entity.GetType())
                && (e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted))
            .ToArray();

        foreach (var entry in entries)
        {
            context.Add(entry.AutoAuditHistory(userId));
        }
    }

    public static AuditHistory AutoAuditHistory(this EntityEntry entry, string userId)
    {
        var history = new AuditHistory()
        {
            UserId = userId,
            EntityType = entry.Metadata.DisplayName(),
            EntityId = entry.GetPrimaryKey()
        };

        var properties = entry.Properties.Where(p => !AuditUtilities.IsNotAuditable(p.EntityEntry.Entity.GetType(), p.Metadata.Name));

        foreach (var property in properties)
        {
            string propertyName = property.Metadata.Name;
            if (property.Metadata.IsPrimaryKey())
            {
                history.NewValues[propertyName] = property.CurrentValue;
                continue;
            }

            switch (entry.State)
            {
                case EntityState.Added:
                    history.Action = AuditHistoryType.Create;
                    history.NewValues.Add(propertyName, property.CurrentValue);
                    break;
                case EntityState.Modified:
                    history.Action = AuditHistoryType.Update;
                    history.OldValues.Add(propertyName, property.OriginalValue);
                    history.NewValues.Add(propertyName, property.CurrentValue);
                    break;
                case EntityState.Deleted:
                    history.Action = AuditHistoryType.Delete;
                    history.OldValues.Add(propertyName, property.OriginalValue);
                    break;
            }
        }

        return history;
    }

    private static string GetPrimaryKey(this EntityEntry entry)
    {
        var values = new List<object>();
        foreach (var property in entry.Metadata.FindPrimaryKey().Properties)
        {
            var value = entry.Property(property.Name).CurrentValue;
            if (value != null)
            {
                values.Add(value);
            }
        }

        return string.Join(",", values);
    }
}