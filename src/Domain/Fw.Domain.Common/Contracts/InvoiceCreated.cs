using Fw.Domain.Common.Enums;

namespace Fw.Domain.Common.Contracts
{
    public record InvoiceCreated
    {
        public Guid InvoiceId { get; init; }
        public InvoiceStatus InvoiceStatus { get; init; }
        public Guid? ShipmentId { get; init; }
        public Guid? OrderId { get; set; }
    }
}