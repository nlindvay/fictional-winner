using Fw.Domain.Common.Interfaces;
using Fw.Domain.Common.Enums;

namespace Fw.Domain.Tms.Entities;

public class Shipment : IEntity, IAuditable, IOwner
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
    public string PrimaryReference { get; set; }
    public string SecondaryReference { get; set; }
    public ShipmentStatus ShipmentStatus { get; set; }
    public ICollection<Pack> Packages { get; set; }

}