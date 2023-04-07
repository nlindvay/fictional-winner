using Fw.Application.Wms.Consumers;
using Fw.Application.Wms.Interfaces;
using Fw.Domain.Common.Contracts;
using Fw.Infrastructure.Persistance.Wms;
using Fw.Presentation.Api.Wms.Services;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
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

builder.ConfigureMassTransit();
builder.ConfigureMediator();

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