using Fw.Application.Ams.Handlers;
using MassTransit;

namespace Fw.Presentation.Api.Ams.Services;

public static partial class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder ConfigureMediator(this WebApplicationBuilder builder)
    {
        builder.Services.AddMediator(cfg =>
        {
            cfg.AddConsumer<SubmitInvoiceHandler>();
            cfg.AddConsumer<SubmitInvoiceLineHandler>();
            cfg.AddConsumer<PaginateInvoicesHandler>();
            cfg.AddConsumer<GetInvoiceHandler>();
        });

        return builder;
    }
}