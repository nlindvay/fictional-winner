using Fw.Infrastructure.Persistance.Common.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Fw.Infrastructure.Persistance.Common;

public class AuditableDbContext : DbContext
{
    public AuditableDbContext(DbContextOptions options) : base(options)
    {
    }

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