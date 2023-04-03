using Fw.Domain.Ams.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fw.Application.Ams.Interfaces;

public interface IAmsDbContext
{
    DbSet<Invoice> Invoices { get; }
    DbSet<InvoiceLine> InvoiceLines { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

}