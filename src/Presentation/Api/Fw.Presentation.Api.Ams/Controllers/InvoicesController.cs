using Fw.Domain.Ams.Contracts;
using Fw.Domain.Common.Enums;
using MassTransit;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace Fw.Presentation.Api.Ams.Controllers;

[ApiController]
[Route("Ams/[controller]")]
public class InvoicesController : ControllerBase
{
    private readonly ILogger<InvoicesController> _logger;
    readonly IMediator _mediator;

    public InvoicesController(ILogger<InvoicesController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpPost()]
    public async Task<IActionResult> SubmitInvoice([FromBody] SubmitInvoice submitInvoice, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Submitting Invoice {PrimaryReference}", submitInvoice.PrimaryReference);

        return Ok(await _mediator.SendRequest(submitInvoice, cancellationToken));

    }

    [HttpGet("{invoiceId}")]
    public async Task<IActionResult> GetInvoice(Guid invoiceId, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting Invoice {invoiceId}", invoiceId);

        return Ok(await _mediator.SendRequest(new GetInvoice { InvoiceId = invoiceId }, cancellationToken));
    }

    [HttpGet()]
    public async Task<IActionResult> PaginateInvoices(int page, int size, SortDirection direction, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting Invoices");

        return Ok(await _mediator.SendRequest(new PaginateInvoices { Page = page, Size = size, Direction = default }, cancellationToken));
    }

    [HttpPost("{invoiceId}/packages")]
    public async Task<IActionResult> AddInvoiceLine(Guid invoiceId, [FromBody] SubmitInvoiceLine submitInvoiceLine, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Adding Package to Invoice {invoiceId}", invoiceId);

        return Ok(await _mediator.SendRequest(submitInvoiceLine, cancellationToken));
    }
}