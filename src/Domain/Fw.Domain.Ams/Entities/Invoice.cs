using Fw.Domain.Common.Enums;
using Fw.Domain.Common.Interfaces;

namespace Fw.Domain.Ams.Entities;

public record Invoice : IEntity, IAuditable, IOwner

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
    public InvoiceStatus InvoiceStatus { get; set; }
    public ICollection<InvoiceLine> InvoiceLines { get; set; }
    public Guid? OrderId { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public Guid? ShipmentId { get; set; }
    public ShipmentStatus ShipmentStatus { get; set; }

}