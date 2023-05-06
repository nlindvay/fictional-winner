using Fw.Infrastructure.Persistance.Common.Extensions;
using Fw.Infrastructure.Persistance.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace Fw.Infrastructure.Persistance.Common;

public abstract class AuditableDbContext : DbContext
{
    public AuditableDbContext(DbContextOptions options) : base(options)
    {
    }

    public virtual DbSet<AuditHistory> AuditHistory { get; set; }
    
    public override int SaveChanges()
    {
        var username = "@Admin";
        this.EnsureAudit(username);
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        var username = "@Admin";
        this.EnsureAudit(username);
        return base.SaveChangesAsync(cancellationToken);
    }

}