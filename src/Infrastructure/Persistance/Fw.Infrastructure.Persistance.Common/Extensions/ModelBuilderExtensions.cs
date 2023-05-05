using System.Text.Json;
using Fw.Infrastructure.Persistance.Common.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Fw.Infrastructure.Persistance.Common.Extensions;

public static class DbContextExtensions
{
    public static void EnsureAudit(this DbContext context, string username)
    {
        var entries = context.ChangeTracker
            .Entries()
            .Where(e => !AuditUtilities.IsNotAuditable(e.Entity.GetType())
                && (e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted))
            .ToArray();

        foreach (var entry in entries)
        {
            context.Add(entry.AutoAuditHistory(username));
        }
    }

    public static AuditHistory AutoAuditHistory(this EntityEntry entry, string username)
    {
        var history = new AuditHistory()
        {
            Username = username,
            EntityType = entry.Metadata.DisplayName()
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
                    history.EntityId = Guid.Empty.ToString();
                    history.Kind = EntityState.Added;
                    history.NewValues.Add(propertyName, property.CurrentValue);
                    break;
                case EntityState.Modified:
                    history.EntityId = entry.GetPrimaryKey();
                    history.Kind = EntityState.Modified;
                    history.OldValues.Add(propertyName, property.OriginalValue);
                    history.NewValues.Add(propertyName, property.CurrentValue);

                    break;
                case EntityState.Deleted:
                    history.EntityId = entry.GetPrimaryKey();
                    history.Kind = EntityState.Deleted;
                    history.OldValues.Add(propertyName, property.OriginalValue);
                    break;
            }
        }

        history.Changed = JsonSerializer.Serialize(new { NewValues = history.NewValues, OldValues = history.OldValues });
        return history;
    }

    private static string GetPrimaryKey(this EntityEntry entry)
    {
        var key = entry.Metadata.FindPrimaryKey();

        var values = new List<object>();
        foreach (var property in key.Properties)
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