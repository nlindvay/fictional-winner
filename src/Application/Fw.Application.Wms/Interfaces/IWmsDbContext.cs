using Fw.Application.Common.Interfaces;
using Fw.Domain.Wms.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fw.Application.Wms.Interfaces;

public interface IWmsDbContext : IAuditableDbContext

{
    DbSet<Order> Orders { get; }
    DbSet<OrderLine> OrderLines { get; }
    DbSet<Sku> Skus { get; }
}