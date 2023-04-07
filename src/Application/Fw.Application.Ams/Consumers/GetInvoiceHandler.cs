using AutoMapper;
using Fw.Application.Ams.Interfaces;
using Fw.Domain.Ams.Contracts;
using Fw.Domain.Common.Dtos;
using MassTransit.Mediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Fw.Application.Ams.Consumers;

public class GetInvoiceHandler : MediatorRequestHandler<GetInvoice, InvoiceDto>
{
    readonly IAmsDbContext _context;
    readonly ILogger<GetInvoiceHandler> _logger;
    readonly IMapper _mapper;

    public GetInvoiceHandler(IAmsDbContext context, ILogger<GetInvoiceHandler> logger, IMapper mapper)
    {
        _context = context;
        _logger = logger;
        _mapper = mapper;
    }

    protected override async Task<InvoiceDto> Handle(GetInvoice request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting Invoice {InvoiceId}", request.InvoiceId);

        var invoice = await _context.Invoices
            .Include(s => s.InvoiceLines)
            .FirstOrDefaultAsync(s => s.Id == request.InvoiceId, cancellationToken);

        return invoice == null ? null : _mapper.Map<InvoiceDto>(invoice);
    }
}