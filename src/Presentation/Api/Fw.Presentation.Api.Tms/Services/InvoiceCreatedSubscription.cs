using Fw.Application.Tms.Consumers;
using MassTransit;
using RabbitMQ.Client;

namespace Fw.Presentation.Api.Tms.Services;

public static partial class IRabbitMqBusFactoryConfiguratorExtensions
{
    public static IRabbitMqBusFactoryConfigurator AddInvoiceCreatedSubscription(this IRabbitMqBusFactoryConfigurator cfg, IBusRegistrationContext context, string subscriptionName)
    {
        cfg.ReceiveEndpoint(subscriptionName, cfg =>
        {
            cfg.ConfigureConsumeTopology = false;
            cfg.SetQuorumQueue();
            cfg.SetQueueArgument("declare", "lazy");
            cfg.Bind("invoice-created", cfg =>
            {
                cfg.ExchangeType = ExchangeType.Topic;
            });
            cfg.Consumer<InvoiceCreatedConsumer>(context, x =>
            {
                x.UseScheduledRedelivery(x => x.Interval(5, TimeSpan.FromSeconds(2)));
            });
        });
        return cfg;
    }
}