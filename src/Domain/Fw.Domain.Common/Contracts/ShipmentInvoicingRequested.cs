using Fw.Domain.Common.Dtos;

namespace Fw.Domain.Common.Contracts;

public record ShipmentInvoicingRequested
{
    public ShipmentDto Shipment { get; init; }
}