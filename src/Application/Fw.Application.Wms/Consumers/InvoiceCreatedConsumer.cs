using Fw.Domain.Common.Contracts;
using MassTransit;

namespace Fw.Application.Wms.Consumers;

public class InvoiceCreatedConsumer : IConsumer<InvoiceCreated>
{
    public Task Consume(ConsumeContext<InvoiceCreated> context)
    {
        throw new NotImplementedException();
    }
}