namespace Fw.Domain.Wms.Entities;

public class OrderLine : IEntity, IAuditable
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
    public int LineNumber { get; set; }
    public int Quantity { get; set; }
    public Guid SkuId { get; set; }
    public Sku Sku { get; set; }
    public Guid OrderId { get; set; }
    public Order Order { get; set; }
}