using AutoMapper;
using Fw.Application.Wms.Interfaces;
using Fw.Domain.Wms.Contracts;
using MassTransit.Mediator;
using Microsoft.Extensions.Logging;

namespace Fw.Application.Wms.Consumers;

public class GetSkuHandler : MediatorRequestHandler<GetSku, SkuDto>
{
    readonly IApplicationDbContext _context;
    private readonly ILogger<GetSkuHandler> _logger;
    readonly IMapper _mapper;

    public GetSkuHandler(IApplicationDbContext context, ILogger<GetSkuHandler> logger, IMapper mapper)
    {
        _context = context;
        _logger = logger;
        _mapper = mapper;
    }


    protected override async Task<SkuDto> Handle(GetSku request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("GetSkuConsumer: {SkuId}", request.SkuId);

        var sku = _context.Skus.FirstOrDefault(sku => sku.Id == request.SkuId);

        return _mapper.Map<SkuDto>(sku) ?? null;
    }
}
