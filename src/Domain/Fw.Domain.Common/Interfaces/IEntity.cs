namespace Fw.Domain.Common.Interfaces;

public interface IEntity
{
    Guid Id { get; set; }
    Guid ClientId { get; set; }
    bool IsActive { get; set; }
    bool IsDeleted { get; set; }
    int Version { get; set; }
}