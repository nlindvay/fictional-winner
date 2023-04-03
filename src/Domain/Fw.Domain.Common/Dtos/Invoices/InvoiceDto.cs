namespace Fw.Domain.Common.Dtos;

public record InvoiceDto

{
    public Guid Id { get; set; }
    public Guid ClientId { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }
    public int Version { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public string LastModifiedBy { get; set; }
    public DateTime? LastModifiedDate { get; set; }
    public Guid CustomerId { get; set; }
    public string PrimaryReference { get; set; }
    public string SecondaryReference { get; set; }
    public string InvoiceNumber { get; set; }
    public InvoiceLineDto[] InvoiceLines { get; set; }
}