using MassTransit.Mediator;

namespace Fw.Domain.Wms.Contracts
{
    public record AddOrderLine: Request<OrderLineAdded>
    {
        public Guid OrderId { get; set; }
        public Guid SkuId { get; set; }
        public int Quantity { get; set; }
    }
}