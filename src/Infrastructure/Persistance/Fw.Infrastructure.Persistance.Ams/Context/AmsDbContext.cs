using Fw.Application.Ams.Interfaces;
using Fw.Domain.Ams.Entities;
using Fw.Infrastructure.Persistance.Ams.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Fw.Infrastructure.Persistance.Ams;

public class AmsDbContext : DbContext, IAmsDbContext

{
    public AmsDbContext(DbContextOptions<AmsDbContext> options) : base(options)
    {
    }

    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<InvoiceLine> InvoiceLines { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new InvoiceConfiguration());
        builder.ApplyConfiguration(new InvoiceLineConfiguration());
        base.OnModelCreating(builder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }
}