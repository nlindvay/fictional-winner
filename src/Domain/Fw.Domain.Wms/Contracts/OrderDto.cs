using Fw.Domain.Wms.Enums;

namespace Fw.Domain.Wms.Contracts
{
    public record OrderDto
    {
        public Guid OrderId { get; set; }
        public Guid ClientId { get; set; }
        public Guid CustomerId { get; set; }
        public string OrderReference { get; set; }
        public string CustomerReference { get; set; }
        public OrderStatus OrderStatus { get; set; }
    }
}