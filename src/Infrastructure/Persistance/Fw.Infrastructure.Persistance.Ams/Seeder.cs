using AutoFixture;
using Fw.Domain.Ams.Entities;

namespace Fw.Infrastructure.Persistance.Ams;

public static class AmsDbContextSeeder
{
    public static void Seed(this AmsDbContext context)
    {
        if (!context.Invoices.Any())
        {
            var fixture = new Fixture();
            fixture.Customize<Invoice>(invoice => invoice.Without(s => s.Id));
            List<Invoice> invoices = fixture.CreateMany<Invoice>(100).ToList();
            context.AddRange(invoices);
        }

        context.SaveChanges();
    }
}