using AutoMapper;
using Fw.Application.Wms.Interfaces;
using Fw.Domain.Wms.Contracts;
using Fw.Domain.Wms.Entities;
using MassTransit;
using MassTransit.Mediator;
using Microsoft.Extensions.Logging;

namespace Fw.Application.Wms.Handlers;

public class SubmitSkuHandler : MediatorRequestHandler<SubmitSku, SkuSubmitted>

{
    readonly IWmsDbContext _context;
    readonly ILogger<SubmitSkuHandler> _logger;
    readonly IMapper _mapper; 

    public SubmitSkuHandler(IWmsDbContext context, ILogger<SubmitSkuHandler> logger, IMapper mapper)
    {
        _context = context;
        _logger = logger;
        _mapper = mapper;
    }

    protected override async Task<SkuSubmitted> Handle(SubmitSku request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("SubmitSkuConsumer: {SkuCode}", request.SkuCode);
        
        var Sku = new Sku
        {
            Id = NewId.NextGuid(),
            ClientId = NewId.NextGuid(),
            CustomerId = NewId.NextGuid(),
            SkuCode = request.SkuCode,
            SkuDescription = request.SkuDescription
        };

        await _context.Skus.AddAsync(Sku, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return new SkuSubmitted
        {
            SkuId = Sku.Id
        };
    }
}