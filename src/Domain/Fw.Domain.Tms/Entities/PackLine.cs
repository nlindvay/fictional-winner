using Fw.Domain.Common.Interfaces;

namespace Fw.Domain.Tms.Entities;

public class PackLine : IEntity, IAuditable, IOwner
{
    public Guid Id { get; set; }
    public Guid ClientId { get; set; } = Guid.Empty;
    public bool IsActive { get; set; } = true;
    public bool IsDeleted { get; set; } = false;
    public string CreatedBy { get; set; } = "Admin@me";
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public string LastModifiedBy { get; set; } = null;
    public DateTime? LastModifiedDate { get; set; } = null;
    public int Version { get; set; } = 1;
    public Guid CustomerId { get; set; }
    public int LineNumber { get; set; }
    public int LineQuantity { get; set; }
    public string LineDescription { get; set; }
    public Guid PackId { get; set; }
    public Pack Pack { get; set; }
}