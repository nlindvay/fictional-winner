namespace Fw.Domain.Common.Contracts
{
    public record ShipmentCreated
    {
        public Guid ShipmentId { get; init; }
        public Guid? OrderId { get; init; }
    }
}