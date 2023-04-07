using Fw.Domain.Common.Enums;

namespace Fw.Domain.Common.Contracts
{
    public record OrderCreated
    {
        public Guid OrderId { get; init; }
        public OrderStatus OrderStatus { get; init; }
    }
}