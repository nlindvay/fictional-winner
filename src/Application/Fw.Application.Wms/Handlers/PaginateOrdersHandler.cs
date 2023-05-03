using MapsterMapper;
using Fw.Application.Wms.Interfaces;
using Fw.Domain.Common.Dtos;
using Fw.Domain.Wms.Contracts;
using MassTransit.Mediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Fw.Application.Wms.Handlers;

public class PaginateOrdersHandler : MediatorRequestHandler<PaginateOrders, OrderDto[]>
{
    readonly IWmsDbContext _context;
    private readonly ILogger<PaginateOrdersHandler> _logger;
    readonly IMapper _mapper;

    public PaginateOrdersHandler(IWmsDbContext context, ILogger<PaginateOrdersHandler> logger, IMapper mapper)
    {
        _context = context;
        _logger = logger;
        _mapper = mapper;
    }
 

    protected override Task<OrderDto[]> Handle(PaginateOrders request, CancellationToken cancellationToken)
    {
        return _context.Orders
            .Include(order => order.OrderLines)
            .Skip(Math.Max(0,(request.Page - 1)) * request.Size)
            .Take(request.Size)
            .Select(order => _mapper.Map<OrderDto>(order))
            .ToArrayAsync(cancellationToken);
    }
}