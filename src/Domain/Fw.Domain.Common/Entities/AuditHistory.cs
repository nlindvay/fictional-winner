using Fw.Domain.Common.Attributes;
using Fw.Domain.Common.Enums;

namespace Fw.Infrastructure.Persistance.Common.Models;

[NotAuditable]
public class AuditHistory
{
    public Guid Id { get; set; }
    public string EntityId { get; set; }
    public string EntityType { get; set; }
    public string RootId { get; set; }
    public string RootType { get; set; }
    public string UserId { get; set; }
    public AuditHistoryType Action { get; set; }
    public DateTime Timestamp { get; set; }
    public Dictionary<string, object> NewValues { get; set; } = new Dictionary<string, object>();
    public Dictionary<string, object> OldValues { get; set; } = new Dictionary<string, object>();
}