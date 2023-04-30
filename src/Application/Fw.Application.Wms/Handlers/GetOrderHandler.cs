using AutoMapper;
using Fw.Application.Wms.Interfaces;
using Fw.Domain.Common.Dtos;
using Fw.Domain.Wms.Contracts;
using MassTransit.Mediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Fw.Application.Wms.Handlers;

public class GetOrderHandler : MediatorRequestHandler<GetOrder, OrderDto>

{
    readonly IWmsDbContext _context;
    private readonly ILogger<GetOrderHandler> _logger;
    readonly IMapper _mapper;

    public GetOrderHandler(IWmsDbContext context, ILogger<GetOrderHandler> logger, IMapper mapper)
    {
        _context = context;
        _logger = logger;
        _mapper = mapper;
    }


    protected override async Task<OrderDto> Handle(GetOrder request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("GetOrderConsumer: {OrderId}", request.OrderId);

        var order = await _context.Orders
            .Include(order => order.OrderLines)
            .FirstOrDefaultAsync(order => order.Id == request.OrderId, cancellationToken);

        return order == null ? null : _mapper.Map<OrderDto>(order);
    }
}