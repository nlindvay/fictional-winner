namespace Fw.Domain.Common.Contracts
{
    public record InvoiceCreated
    {
        public Guid InvoiceId { get; init; }
        public Guid? ShipmentId { get; init; }
    }
}