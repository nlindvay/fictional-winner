using Fw.Application.Tms.Consumers;
using MassTransit;

namespace Fw.Presentation.Api.Tms.Services;

public static partial class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder ConfigureMediator(this WebApplicationBuilder builder)
    {
        builder.Services.AddMediator(cfg =>
        {
            cfg.AddConsumer<SubmitShipmentHandler>();
            cfg.AddConsumer<SubmitPackHandler>();
            cfg.AddConsumer<SubmitPackLineHandler>();
            cfg.AddConsumer<PaginateShipmentsHandler>();
            cfg.AddConsumer<GetShipmentHandler>();
            cfg.AddConsumer<RequestShipmentInvoicingHandler>();
        });

        return builder;
    }
}