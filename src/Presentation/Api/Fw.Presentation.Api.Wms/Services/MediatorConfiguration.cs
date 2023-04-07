using Fw.Application.Wms.Consumers;
using MassTransit;

namespace Fw.Presentation.Api.Wms.Services;

public static partial class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder ConfigureMediator(this WebApplicationBuilder builder)
    {
        builder.Services.AddMediator(cfg =>
        {
            cfg.AddConsumer<RequestShipOrderHandler>();
            cfg.AddConsumer<SubmitOrderHandler>();
            cfg.AddConsumer<GetOrderHandler>();
            cfg.AddConsumer<PaginateOrdersHandler>();
            cfg.AddConsumer<AddOrderLineHandler>();
            cfg.AddConsumer<SubmitSkuHandler>();
            cfg.AddConsumer<GetSkuHandler>();
            cfg.AddConsumer<PaginateSkusHandler>();
        });

        return builder;
    }
}