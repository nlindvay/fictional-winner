using Fw.Domain.Wms.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fw.Application.Wms.Interfaces;

public interface IWmsDbContext
{
    DbSet<Order> Orders { get; }
    DbSet<OrderLine> OrderLines { get; }
    DbSet<Receive> Receives { get; }
    DbSet<ReceiveLine> ReceiveLines { get; }
    DbSet<Sku> Skus { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}