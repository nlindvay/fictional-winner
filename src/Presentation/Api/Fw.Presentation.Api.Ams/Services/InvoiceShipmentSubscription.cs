using Fw.Application.Ams.Consumers;
using MassTransit;
using RabbitMQ.Client;

namespace Fw.Presentation.Api.Ams.Services;

public static partial class IRabbitMqBusFactoryConfiguratorExtensions
{
    public static IRabbitMqBusFactoryConfigurator AddInvoiceShipmentSubscription(this IRabbitMqBusFactoryConfigurator cfg, IBusRegistrationContext context, string subscriptionName)
    {
        cfg.ReceiveEndpoint(subscriptionName, cfg =>
        {
            cfg.ConfigureConsumeTopology = false;
            cfg.SetQuorumQueue();
            cfg.SetQueueArgument("declare", "lazy");
            cfg.Bind("invoice-shipment", cfg =>
            {
                cfg.ExchangeType = ExchangeType.Topic;
            });
            cfg.Consumer<InvoiceShipmentConsumer>(context, x =>
            {
                x.UseScheduledRedelivery(x => x.Interval(5, TimeSpan.FromSeconds(2)));
            });
        });

        return cfg;
    }
}