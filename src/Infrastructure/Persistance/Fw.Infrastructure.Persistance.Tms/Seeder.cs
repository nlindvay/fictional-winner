using AutoFixture;
using Fw.Domain.Tms.Entities;

namespace Fw.Infrastructure.Persistance.Tms;

public static class TmsDbContextSeeder
{
    public static void Seed(this TmsDbContext context)
    {
        if (!context.Shipments.Any())
        {
            var fixture = new Fixture();
            fixture.Customize<Shipment>(shipment => shipment.Without(s => s.Id));
            List<Shipment> shipments = fixture.CreateMany<Shipment>(100).ToList();
            context.AddRange(shipments);
        }

        context.SaveChanges();
    }
}