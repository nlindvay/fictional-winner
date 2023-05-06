using Fw.Infrastructure.Persistance.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace Fw.Application.Common.Interfaces;

public interface IAuditableDbContext
{
    DbSet<AuditHistory> AuditHistory { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}