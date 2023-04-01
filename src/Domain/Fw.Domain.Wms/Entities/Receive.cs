using Fw.Domain.Wms.Enums;

namespace Fw.Domain.Wms.Entities;

public class Receive : IEntity, IAuditable
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
    public string PrimaryReference { get; set; }
    public string SecondaryReference { get; set; }
    public Guid CustomerId { get; set; }
    public ReceiveStatus ReceiveStatus { get; set; }
    public ICollection<ReceiveLine> ReceiveLines { get; set; }
}