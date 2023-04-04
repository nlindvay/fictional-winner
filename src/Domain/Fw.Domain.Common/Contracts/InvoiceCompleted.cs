namespace Fw.Domain.Common.Contracts
{
    public record InvoiceCompleted
    {
        public Guid InvoiceId { get; init; }
    }
}