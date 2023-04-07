using Fw.Domain.Common.Dtos;

namespace Fw.Domain.Common.Contracts;

public record InvoiceShipment
{
    public ShipmentDto Shipment { get; init; }
}