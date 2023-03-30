using Fw.Application.Wms.Consumers;
using Fw.Application.Wms.Interfaces;
using Fw.Infrastructure.Persistance.Wms;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("WmsDb"));
builder.Services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

builder.Services.AddMediator(cfg =>
{
    cfg.AddConsumer<SubmitOrderConsumer>();
    cfg.AddConsumer<GetOrderConsumer>();
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

app.Run();