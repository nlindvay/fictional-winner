namespace Fw.Domain.Common.Contracts
{
    public record OrderCompleted
    {
        public Guid OrderId { get; init; }
    }
}