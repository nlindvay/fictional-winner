using MassTransit.Mediator;

namespace Fw.Domain.Wms.Contracts
{
    public record GetSku : Request<SkuDto>
    {
        public Guid SkuId { get; set; }
    }
}