using Fw.Domain.Common.Enums;
using Fw.Domain.Common.Dtos;
using MassTransit.Mediator;

namespace Fw.Domain.Ams.Contracts;

public class PaginateInvoices : Request<InvoiceDto[]>

{
    public int Page { get; set; }
    public int Size { get; set; }
    public SortDirection Direction { get; set; }
}