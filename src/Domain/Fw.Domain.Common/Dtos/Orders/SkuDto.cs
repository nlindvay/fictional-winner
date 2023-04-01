namespace Fw.Domain.Common.Dtos;

public record SkuDto
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
    public string SkuCode { get; set; }
    public string SkuDescription { get; set; }

}