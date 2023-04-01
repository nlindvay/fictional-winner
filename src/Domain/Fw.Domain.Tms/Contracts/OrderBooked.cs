namespace Fw.Domain.Tms.Contracts;

public record OrderBooked
{
    public Guid OrderId { get; init; }
    public Guid ShipmentId { get; init; }
}