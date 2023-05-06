using Fw.Application.Common.Interfaces;
using Fw.Domain.Ams.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fw.Application.Ams.Interfaces;

public interface IAmsDbContext : IAuditableDbContext
{
    DbSet<Invoice> Invoices { get; }
    DbSet<InvoiceLine> InvoiceLines { get; }
}