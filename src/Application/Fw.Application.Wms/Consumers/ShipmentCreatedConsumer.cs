using Fw.Domain.Common.Contracts;
using MassTransit;

namespace Fw.Application.Wms.Consumers;

public class ShipmentCreatedConsumer : IConsumer<ShipmentCreated>
{
    public Task Consume(ConsumeContext<ShipmentCreated> context)
    {
        throw new NotImplementedException();
    }
}