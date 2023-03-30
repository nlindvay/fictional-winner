using MassTransit.Mediator;

namespace Fw.Domain.Wms.Contracts
{
    public record SubmitOrder : Request<OrderSubmitted>
    {
        public string OrderReference { get; set; }
        public string CustomerReference { get; set; }
    }
}