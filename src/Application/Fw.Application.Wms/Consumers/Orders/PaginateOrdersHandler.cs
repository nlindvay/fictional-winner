using AutoMapper;
using Fw.Application.Wms.Interfaces;
using Fw.Domain.Wms.Contracts;
using MassTransit.Mediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Fw.Application.Wms.Consumers;

public class PaginateOrdersHandler : MediatorRequestHandler<PaginateOrders, OrderDto[]>
{
    readonly IApplicationDbContext _context;
    private readonly ILogger<PaginateOrdersHandler> _logger;
    readonly IMapper _mapper;

    public PaginateOrdersHandler(IApplicationDbContext context, ILogger<PaginateOrdersHandler> logger, IMapper mapper)
    {
        _context = context;
        _logger = logger;
        _mapper = mapper;
    }
 

    protected override Task<OrderDto[]> Handle(PaginateOrders request, CancellationToken cancellationToken)
    {
        return _context.Orders.Include(order => order.OrderLines).Skip(request.Page * request.Size)
            .Take(request.Size)
            .Select(order => _mapper.Map<OrderDto>(order))
            .ToArrayAsync(cancellationToken);
    }
}