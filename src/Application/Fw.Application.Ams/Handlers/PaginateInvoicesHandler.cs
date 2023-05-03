using MapsterMapper;
using Fw.Application.Ams.Interfaces;
using Fw.Domain.Ams.Contracts;
using Fw.Domain.Common.Dtos;
using MassTransit.Mediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Fw.Application.Ams.Handlers;

public class PaginateInvoicesHandler : MediatorRequestHandler<PaginateInvoices, InvoiceDto[]>
{
    readonly IAmsDbContext _context;
    readonly ILogger<PaginateInvoicesHandler> _logger;
    readonly IMapper _mapper;

    public PaginateInvoicesHandler(IAmsDbContext context, ILogger<PaginateInvoicesHandler> logger, IMapper mapper)
    {
        _context = context;
        _logger = logger;
        _mapper = mapper;
    }

    protected override async Task<InvoiceDto[]> Handle(PaginateInvoices request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Paginating Invoices {Page} {Size}", request.Page, request.Size);

        return await _context.Invoices
            .Include(i => i.InvoiceLines)
            .Skip(Math.Max(0,(request.Page - 1)) * request.Size)
            .Take(request.Size)
            .Select(i => _mapper.Map<InvoiceDto>(i))
            .ToArrayAsync(cancellationToken);
    }
}