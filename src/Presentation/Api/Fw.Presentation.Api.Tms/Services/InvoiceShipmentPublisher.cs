
using Fw.Domain.Common.Contracts;
using MassTransit;
using RabbitMQ.Client;

namespace Fw.Presentation.Api.Tms.Services;

public static partial class IRabbitMqBusFactoryConfiguratorExtensions
{
    public static IRabbitMqBusFactoryConfigurator AddInvoiceShipmentPublisher(this IRabbitMqBusFactoryConfigurator cfg, string topicName)
    {
        cfg.Message<InvoiceShipment>(cfg => cfg.SetEntityName(topicName));
        cfg.Publish<InvoiceShipment>(cfg => cfg.ExchangeType = ExchangeType.Topic);
        return cfg;
    }
}