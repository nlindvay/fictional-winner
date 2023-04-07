using Fw.Domain.Common.Enums;

namespace Fw.Domain.Common.Dtos;

public record OrderDto
{
    public Guid Id { get; set; }
    public Guid ClientId { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public string LastModifiedBy { get; set; }
    public DateTime? LastModifiedDate { get; set; }
    public int Version { get; set; }
    public string PrimaryReference { get; set; }
    public string SecondaryReference { get; set; }
    public Guid CustomerId { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public OrderLineDto[] OrderLines { get; set; }

    public Guid? ShipmentId { get; set; }
    public ShipmentStatus ShipmentStatus { get; set; }
    public Guid? InvoiceId { get; set; }
    public InvoiceStatus InvoiceStatus { get; set; }
}