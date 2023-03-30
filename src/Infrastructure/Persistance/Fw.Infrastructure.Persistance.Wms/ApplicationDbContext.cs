using Fw.Application.Wms.Interfaces;
using Fw.Domain.Wms.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fw.Infrastructure.Persistance.Wms;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Order> Orders { get; set; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }
}