using Fw.Domain.Common.Interfaces;

namespace Fw.Domain.Tms.Entities;

public class Pack : IEntity, IAuditable, IOwner
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
    public ICollection<PackLine> PackLines { get; set; }
    public Guid ShipmentId { get; set; }
    public Shipment Shipment { get; set; }
}