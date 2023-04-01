using AutoMapper;
using Fw.Application.Wms.Interfaces;
using Fw.Domain.Wms.Contracts;
using MassTransit.Mediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Fw.Application.Wms.Consumers;

public class GetOrderHandler : MediatorRequestHandler<GetOrder, OrderDto>
{
    readonly IApplicationDbContext _context;
    private readonly ILogger<GetOrderHandler> _logger;
    readonly IMapper _mapper;

    public GetOrderHandler(IApplicationDbContext context, ILogger<GetOrderHandler> logger, IMapper mapper)
    {
        _context = context;
        _logger = logger;
        _mapper = mapper;
    }


    protected override async Task<OrderDto> Handle(GetOrder request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("GetOrderConsumer: {OrderId}", request.OrderId);

        var order = _context.Orders.Include(order => order.OrderLines).FirstOrDefault(order => order.Id == request.OrderId);

        return _mapper.Map<OrderDto>(order) ?? null;
    }
}
