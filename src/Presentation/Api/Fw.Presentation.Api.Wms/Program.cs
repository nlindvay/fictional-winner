using Fw.Application.Wms.Consumers;
using Fw.Application.Wms.Interfaces;
using Fw.Infrastructure.Persistance.Wms;
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

builder.Services.AddDbContext<WmsDbContext>(options => options.UseInMemoryDatabase("WmsDb"));
builder.Services.AddScoped<IWmsDbContext>(provider => provider.GetService<WmsDbContext>());

builder.Services.AddMassTransit(cfg =>
{
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
    cfg.AddConsumer<RequestOrderBookingHandler>();
    cfg.AddConsumer<SubmitOrderHandler>();
    cfg.AddConsumer<GetOrderHandler>();
    cfg.AddConsumer<PaginateOrdersHandler>();
    cfg.AddConsumer<AddOrderLineHandler>();
    cfg.AddConsumer<SubmitSkuHandler>();
    cfg.AddConsumer<GetSkuHandler>();
    cfg.AddConsumer<PaginateSkusHandler>();
});


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Fw.Presentation.Api.Wms v1"));
}

app.MapControllers();

app.UseSerilogRequestLogging();

app.Run();