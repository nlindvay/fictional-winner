using Fw.Application.Ams.Interfaces;
using Fw.Domain.Ams.Entities;
using Fw.Infrastructure.Persistance.Ams.Configurations;
using Fw.Infrastructure.Persistance.Common;
using Fw.Infrastructure.Persistance.Common.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Fw.Infrastructure.Persistance.Ams;

public class AmsDbContext : AuditableDbContext, IAmsDbContext
{
    public AmsDbContext(DbContextOptions<AmsDbContext> options) : base(options)
    {
    }

    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<InvoiceLine> InvoiceLines { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new AuditHistoryConfiguration());
        builder.ApplyConfiguration(new InvoiceConfiguration());
        builder.ApplyConfiguration(new InvoiceLineConfiguration());
        base.OnModelCreating(builder);
    }

}