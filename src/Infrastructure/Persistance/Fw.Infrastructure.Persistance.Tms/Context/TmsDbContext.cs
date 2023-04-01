using Fw.Application.Tms.Interfaces;
using Fw.Domain.Tms.Entities;
using Fw.Infrastructure.Persistance.Tms.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Fw.Infrastructure.Persistance.Tms;

public class TmsDbContext : DbContext, ITmsDbContext

{
    public TmsDbContext(DbContextOptions<TmsDbContext> options) : base(options)
    {
    }

    public DbSet<Shipment> Shipments { get; set; }
    public DbSet<Pack> Packs { get; set; }
    public DbSet<PackLine> PackLines { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new ShipmentConfiguration());
        builder.ApplyConfiguration(new PackConfiguration());
        builder.ApplyConfiguration(new PackLineConfiguration());
        base.OnModelCreating(builder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }
}