using Fw.Domain.Wms.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fw.Application.Wms.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Order> Orders { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}