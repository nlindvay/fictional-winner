namespace Fw.Domain.Common.Interfaces;

public interface IAuditable
{
    public string CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public string LastModifiedBy { get; set; }
    public DateTime? LastModifiedDate { get; set; }
    public int Version { get; set; }
}