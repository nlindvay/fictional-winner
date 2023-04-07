using Fw.Application.Wms.Consumers;
using MassTransit;

namespace Fw.Presentation.Api.Wms.Services;

public static partial class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder ConfigureMassTransit(this WebApplicationBuilder builder)
    {
        builder.Services.AddMassTransit(cfg =>
        {
            cfg.AddConsumer<ShipmentCreatedConsumer>();
            cfg.AddConsumer<InvoiceCreatedConsumer>();

            cfg.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("localhost", "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
                
                cfg.AddShipOrderPublisher("ship-order");
                cfg.AddShipmentCreatedSubscription(context, "wms-shipment-created");
                cfg.AddInvoiceCreatedSubscription(context, "wms-invoice-created");
            });
        });
        
        return builder;
    }
}