
using Fw.Domain.Common.Contracts;
using MassTransit;
using RabbitMQ.Client;

namespace Fw.Presentation.Api.Ams.Services;

public static partial class IRabbitMqBusFactoryConfiguratorExtensions
{
    public static IRabbitMqBusFactoryConfigurator AddInvoiceCreatedPublisher(this IRabbitMqBusFactoryConfigurator cfg, string topicName)
    {
        cfg.Message<InvoiceCreated>(cfg => cfg.SetEntityName(topicName));
        cfg.Publish<InvoiceCreated>(cfg => cfg.ExchangeType = ExchangeType.Topic);
        return cfg;
    }
}