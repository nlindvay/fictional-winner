using Fw.Application.Common.Interfaces;
using Fw.Domain.Tms.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fw.Application.Tms.Interfaces;

public interface ITmsDbContext : IAuditableDbContext
{
    DbSet<Shipment> Shipments { get; }
    DbSet<Pack> Packs { get; }
    DbSet<PackLine> PackLines { get; }
}