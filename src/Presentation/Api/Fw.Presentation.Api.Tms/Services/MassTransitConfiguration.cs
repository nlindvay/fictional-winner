using Fw.Application.Tms.Consumers;
using MassTransit;

namespace Fw.Presentation.Api.Tms.Services;

public static partial class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder ConfigureMassTransit(this WebApplicationBuilder builder)
    {
        builder.Services.AddMassTransit(cfg =>
        {
            cfg.AddConsumer<InvoiceCreatedConsumer>();
            cfg.AddConsumer<ShipOrderConsumer>();

            cfg.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("localhost", "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                cfg.AddInvoiceShipmentPublisher("invoice-shipment");
                cfg.AddShipmentCreatedPublisher("shipment-created");
                
                cfg.AddShipOrderSubscription(context, "tms-order-shipment-requested");
                cfg.AddInvoiceCreatedSubscription(context, "tms-invoice-created");

            });
        });
        
        return builder;
    }
}