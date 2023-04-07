using Fw.Application.Wms.Consumers;
using MassTransit;
using RabbitMQ.Client;

namespace Fw.Presentation.Api.Wms.Services;

public static partial class IRabbitMqBusFactoryConfiguratorExtensions
{
    public static IRabbitMqBusFactoryConfigurator AddShipmentCreatedSubscription(this IRabbitMqBusFactoryConfigurator cfg, IBusRegistrationContext context, string subscriptionName)
    {
        cfg.ReceiveEndpoint(subscriptionName, cfg =>
        {
            cfg.ConfigureConsumeTopology = false;
            cfg.SetQuorumQueue();
            cfg.SetQueueArgument("declare", "lazy");
            cfg.Bind("shipment-created", cfg =>
            {
                cfg.ExchangeType = ExchangeType.Topic;
            });
            cfg.Consumer<ShipmentCreatedConsumer>(context, x =>
            {
                x.UseScheduledRedelivery(x => x.Interval(5, TimeSpan.FromSeconds(2)));
            });
        });

        return cfg;
    }
}