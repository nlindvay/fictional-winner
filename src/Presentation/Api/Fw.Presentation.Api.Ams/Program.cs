using System.Diagnostics;
using Fw.Application.Ams.Interfaces;
using Fw.Infrastructure.Persistance.Ams;
using Fw.Presentation.Api.Ams.Services;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry;
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
            .ConfigureResource(resource => resource
                .AddService(DiagnosticsConfig.ServiceName)
                .AddTelemetrySdk())
            .AddSource("MassTransit")
            .AddSource(DiagnosticsConfig.ActivitySource.Name)
            .AddAspNetCoreInstrumentation()
            .AddJaegerExporter(o =>
            {
                o.AgentPort = 6831;
                o.MaxPayloadSizeInBytes = 4096;
                o.ExportProcessorType = ExportProcessorType.Batch;
                o.BatchExportProcessorOptions = new BatchExportProcessorOptions<Activity>
                {
                    MaxQueueSize = 2048,
                    ScheduledDelayMilliseconds = 5000,
                    ExporterTimeoutMilliseconds = 30000,
                    MaxExportBatchSize = 512,
                };
            }));

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddMaps(assemblies);
});

builder.Services.AddDbContext<AmsDbContext>(options => options.UseInMemoryDatabase("AmsDb"));
builder.Services.AddScoped<IAmsDbContext>(provider => provider.GetService<AmsDbContext>());

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

public static class DiagnosticsConfig
{
    public const string ServiceName = "Ams";
    public static ActivitySource ActivitySource = new ActivitySource(ServiceName);
}