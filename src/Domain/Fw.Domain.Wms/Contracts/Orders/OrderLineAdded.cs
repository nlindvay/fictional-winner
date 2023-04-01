namespace Fw.Domain.Wms.Contracts
{
    public record OrderLineAdded
    {
        public Guid OrderId { get; set; }
        public Guid OrderLineId { get; set; }
    }
}