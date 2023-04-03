using Fw.Domain.Common.Dtos;
using MassTransit.Mediator;

namespace Fw.Domain.Ams.Contracts;

public class GetInvoice : Request<InvoiceDto>
{
    public Guid InvoiceId { get; set; }
}