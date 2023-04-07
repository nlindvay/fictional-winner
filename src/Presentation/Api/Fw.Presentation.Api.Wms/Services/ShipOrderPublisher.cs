using Fw.Application.Wms.Consumers;
using Fw.Domain.Common.Contracts;
using MassTransit;
using RabbitMQ.Client;

namespace Fw.Presentation.Api.Wms.Services;

public static partial class IRabbitMqBusFactoryConfiguratorExtensions
{
    public static IRabbitMqBusFactoryConfigurator AddShipOrderPublisher(this IRabbitMqBusFactoryConfigurator cfg, string topicName)
    {
        cfg.Message<ShipOrder>(cfg => cfg.SetEntityName(topicName));
        cfg.Publish<ShipOrder>(cfg => cfg.ExchangeType = ExchangeType.Topic);
        return cfg;
    }
}