namespace Fw.Domain.Common.Contracts
{
    public record ShipmentCompleted
    {
        public Guid ShipmentId { get; init; }
    }
}