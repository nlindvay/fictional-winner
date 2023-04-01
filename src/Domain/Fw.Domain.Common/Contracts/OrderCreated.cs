namespace Fw.Domain.Common.Contracts
{
    public record OrderCreated
    {
        public Guid OrderId { get; init; }
    }
}