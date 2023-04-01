using Fw.Domain.Tms.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fw.Application.Tms.Interfaces;

public interface ITmsDbContext
{
    DbSet<Shipment> Shipments { get; }
    DbSet<Pack> Packs { get; }
    DbSet<PackLine> PackLines { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}