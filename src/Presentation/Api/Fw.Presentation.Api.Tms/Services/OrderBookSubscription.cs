using Fw.Application.Tms.Consumers;
using MassTransit;
using RabbitMQ.Client;

namespace Fw.Presentation.Api.Tms.Services;

public static partial class IRabbitMqBusFactoryConfiguratorExtensions
{
    public static IRabbitMqBusFactoryConfigurator AddShipOrderSubscription(this IRabbitMqBusFactoryConfigurator cfg, IBusRegistrationContext context, string subscriptionName)
    {
        cfg.ReceiveEndpoint("tms-ship-order", cfg =>
        {
            cfg.ConfigureConsumeTopology = false;
            cfg.SetQuorumQueue();
            cfg.SetQueueArgument("declare", "lazy");
            cfg.Bind("ship-order", cfg =>
            {
                cfg.ExchangeType = ExchangeType.Topic;
            });
            cfg.Consumer<ShipOrderConsumer>(context, x =>
            {
                x.UseScheduledRedelivery(x => x.Interval(5, TimeSpan.FromSeconds(2)));
            });
        });

        return cfg;
    }
}