using Fw.Application.Ams.Consumers;
using MassTransit;

namespace Fw.Presentation.Api.Ams.Services;

public static partial class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder ConfigureMassTransit(this WebApplicationBuilder builder)
    {
        builder.Services.AddMassTransit(cfg =>
        {
            cfg.AddConsumer<InvoiceShipmentConsumer>();

            cfg.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("localhost", "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                cfg.AddInvoiceCreatedPublisher("invoice-created");
                cfg.AddInvoiceShipmentSubscription(context, "ams-invoice-shipment");
            });
        });
        
        return builder;
    }
}