using Fw.Application.Wms.Interfaces;
using Fw.Domain.Wms.Entities;
using Fw.Infrastructure.Persistance.Wms.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Fw.Infrastructure.Persistance.Wms;

public class WmsDbContext : DbContext, IWmsDbContext
{
    public WmsDbContext(DbContextOptions<WmsDbContext> options) : base(options)
    {
    }

    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderLine> OrderLines { get; set; }
    public DbSet<Sku> Skus { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new OrderConfiguration());
        builder.ApplyConfiguration(new OrderLineConfiguration());
        base.OnModelCreating(builder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }
}