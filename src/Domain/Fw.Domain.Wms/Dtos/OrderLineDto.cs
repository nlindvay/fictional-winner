namespace Fw.Domain.Wms.Contracts;

public record OrderLineDto
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
    public int LineNumber { get; set; }
    public int Quantity { get; set; }
    public Guid SkuId { get; set; }
    public Guid OrderId { get; set; }
}