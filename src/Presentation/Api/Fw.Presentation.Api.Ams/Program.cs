using Fw.Application.Ams.Consumers;
using Fw.Application.Ams.Interfaces;
using Fw.Domain.Common.Contracts;
using Fw.Infrastructure.Persistance.Ams;
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

builder.Services.AddDbContext<AmsDbContext>(options => options.UseInMemoryDatabase("AmsDb"));
builder.Services.AddScoped<IAmsDbContext>(provider => provider.GetService<AmsDbContext>());

builder.Services.AddMassTransit(cfg =>
{

    cfg.AddConsumer<ShipmentInvoicingRequestedConsumer>();

    cfg.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ConfigureEndpoints(context, new KebabCaseEndpointNameFormatter("ams", false));
    });
});

builder.Services.AddMediator(cfg =>
{
    cfg.AddConsumer<SubmitInvoiceHandler>();
    cfg.AddConsumer<SubmitInvoiceLineHandler>();
    cfg.AddConsumer<PaginateInvoicesHandler>();
    cfg.AddConsumer<GetInvoiceHandler>();
});


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.UseSerilogRequestLogging();

app.Run();