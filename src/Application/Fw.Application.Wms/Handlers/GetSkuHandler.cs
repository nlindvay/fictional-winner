using MapsterMapper;
using Fw.Application.Wms.Interfaces;
using Fw.Domain.Common.Dtos;
using Fw.Domain.Wms.Contracts;
using MassTransit.Mediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Fw.Application.Wms.Handlers;

public class GetSkuHandler : MediatorRequestHandler<GetSku, SkuDto>

{
    readonly IWmsDbContext _context;
    private readonly ILogger<GetSkuHandler> _logger;
    readonly IMapper _mapper;

    public GetSkuHandler(IWmsDbContext context, ILogger<GetSkuHandler> logger, IMapper mapper)
    {
        _context = context;
        _logger = logger;
        _mapper = mapper;
    }


    protected override async Task<SkuDto> Handle(GetSku request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("GetSkuConsumer: {SkuId}", request.SkuId);

        var sku = await _context.Skus
            .FirstOrDefaultAsync(sku => sku.Id == request.SkuId, cancellationToken);

        return sku == null ? null : _mapper.Map<SkuDto>(sku);
    }
}
