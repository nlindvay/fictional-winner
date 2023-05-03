using System.Diagnostics;
using Fw.Application.Wms.Interfaces;
using Fw.Infrastructure.Persistance.Wms;
using Fw.Presentation.Api.Wms.Services;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
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

builder.Services.AddOpenTelemetry()
    .WithTracing(tracerProviderBuilder =>
        tracerProviderBuilder
            .AddSource("MassTransit")
            .AddSource(DiagnosticsConfig.ActivitySource.Name)
            .ConfigureResource(resource => resource
                .AddService(DiagnosticsConfig.ServiceName)
                .AddTelemetrySdk())
            .AddAspNetCoreInstrumentation()
            .AddJaegerExporter());

builder.Services.AddSingleton(new TypeAdapterConfig());
builder.Services.AddScoped<IMapper, ServiceMapper>();

builder.Services.AddDbContext<WmsDbContext>(options => options.UseSqlServer("Server=localhost;Database=WmsDb;User Id=SA;Password=A&VeryComplex123Password;MultipleActiveResultSets=true"));
builder.Services.AddScoped<IWmsDbContext>(provider => provider.GetService<WmsDbContext>());

builder.ConfigureMassTransit();
builder.ConfigureMediator();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var wmsDbContext = scope.ServiceProvider.GetRequiredService<WmsDbContext>();
        wmsDbContext.Database.EnsureCreated();
        // wmsDbContext.Seed();
    }

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.UseSerilogRequestLogging();

app.Run();

public static class DiagnosticsConfig
{
    public const string ServiceName = "Wms";
    public static ActivitySource ActivitySource = new ActivitySource(ServiceName);
}