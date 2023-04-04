using Fw.Application.Tms.Consumers;
using Fw.Application.Tms.Interfaces;
using Fw.Domain.Common.Contracts;
using Fw.Infrastructure.Persistance.Tms;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Serilog;


var assemblies = AppDomain.CurrentDomain.GetAssemblies();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, cfg) =>
{
    if (context.HostingEnvironment.IsDevelopment())
    {
        cfg.MinimumLevel.Debug();
        cfg.WriteTo.Console();
    }
    else
    {
        cfg.MinimumLevel.Information();
        cfg.WriteTo.Console();
    }

    cfg.WriteTo.Console();
});

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddMaps(assemblies);
});

builder.Services.AddDbContext<TmsDbContext>(options => options.UseInMemoryDatabase("TmsDb"));
builder.Services.AddScoped<ITmsDbContext>(provider => provider.GetService<TmsDbContext>());

builder.Services.AddMassTransit(cfg =>
{

    cfg.AddConsumer<OrderBookingRequestedConsumer>();

    cfg.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ConfigureEndpoints(context, new KebabCaseEndpointNameFormatter("wms", false));
    });
});

builder.Services.AddMediator(cfg =>
{
    cfg.AddConsumer<SubmitShipmentHandler>();
    cfg.AddConsumer<SubmitPackHandler>();
    cfg.AddConsumer<SubmitPackLineHandler>();
    cfg.AddConsumer<PaginateShipmentsHandler>();
    cfg.AddConsumer<GetShipmentHandler>();
    cfg.AddConsumer<RequestShipmentInvoicingHandler>();
});


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Fw.Presentation.Api.Tms v1"));
}

app.MapControllers();

app.UseSerilogRequestLogging();

app.Run();