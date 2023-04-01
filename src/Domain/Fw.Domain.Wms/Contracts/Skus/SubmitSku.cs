using MassTransit.Mediator;

namespace Fw.Domain.Wms.Contracts;

public record SubmitSku : Request<SkuSubmitted>
{
    public string SkuCode { get; set; }
    public string SkuDescription { get; set; }
}