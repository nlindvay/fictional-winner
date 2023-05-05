using Fw.Infrastructure.Persistance.Common.Attributes;
using Microsoft.EntityFrameworkCore;

namespace Fw.Infrastructure.Persistance.Common.Models;

[NotAuditable]
public class AuditHistory
{
    public Guid Id { get; set; }
    public string EntityId { get; set; }
    public string EntityType { get; set; }
    public string Changed { get; set; }
    public EntityState Kind { get; set; }
    public DateTimeOffset Created { get; set; }
    public string Username { get; set; }
    public Dictionary<string, object> NewValues { get; set; } = new Dictionary<string, object>();
    public Dictionary<string, object> OldValues { get; set; } = new Dictionary<string, object>();
}