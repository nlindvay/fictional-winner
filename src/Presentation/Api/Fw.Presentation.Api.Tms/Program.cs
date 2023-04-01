using Fw.Application.Tms.Consumers;
using Fw.Application.Tms.Interfaces;
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

builder.Services.AddMediator(cfg =>
{
    cfg.AddConsumer<SubmitShipmentConsumer>();
    cfg.AddConsumer<SubmitPackConsumer>();
    cfg.AddConsumer<SubmitPackLineConsumer>();
    cfg.AddConsumer<PaginateShipmentsConsumer>();
    cfg.AddConsumer<GetShipmentConsumer>();
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