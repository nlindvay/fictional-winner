using AutoMapper;
using Fw.Application.Wms.Interfaces;
using Fw.Domain.Wms.Contracts;
using MassTransit.Mediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Fw.Application.Wms.Consumers;

public class PaginateSkusHandler : MediatorRequestHandler<PaginateSkus, SkuDto[]>
{
    readonly IApplicationDbContext _context;
    private readonly ILogger<PaginateSkusHandler> _logger;
    readonly IMapper _mapper;

    public PaginateSkusHandler(IApplicationDbContext context, ILogger<PaginateSkusHandler> logger, IMapper mapper)
    {
        _context = context;
        _logger = logger;
        _mapper = mapper;
    }
 

    protected override Task<SkuDto[]> Handle(PaginateSkus request, CancellationToken cancellationToken)
    {
        return _context.Skus.Skip(request.Page * request.Size)
            .Take(request.Size)
            .Select(sku => _mapper.Map<SkuDto>(sku))
            .ToArrayAsync(cancellationToken);
    }
}