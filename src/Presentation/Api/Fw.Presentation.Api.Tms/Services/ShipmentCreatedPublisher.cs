
using Fw.Domain.Common.Contracts;
using MassTransit;
using RabbitMQ.Client;

namespace Fw.Presentation.Api.Tms.Services;

public static partial class IRabbitMqBusFactoryConfiguratorExtensions
{
    public static IRabbitMqBusFactoryConfigurator AddShipmentCreatedPublisher(this IRabbitMqBusFactoryConfigurator cfg, string topicName)
    {
        cfg.Message<ShipmentCreated>(cfg => cfg.SetEntityName(topicName));
        cfg.Publish<ShipmentCreated>(cfg => cfg.ExchangeType = ExchangeType.Topic);
        return cfg;
    }
}