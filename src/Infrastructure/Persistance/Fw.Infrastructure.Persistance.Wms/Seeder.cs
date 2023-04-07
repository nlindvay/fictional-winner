using AutoFixture;
using Fw.Domain.Wms.Entities;

namespace Fw.Infrastructure.Persistance.Wms;

public static class WmsDbContextSeeder
{
    public static void Seed(this WmsDbContext context)
    {
        if (!context.Skus.Any())
        {
            var fixture = new Fixture();
            fixture.Customize<Sku>(sku => sku.Without(s => s.Id));
            List<Sku> skus = fixture.CreateMany<Sku>(100).ToList();
            context.AddRange(skus);
        }

        if (!context.Orders.Any())
        {
            var fixture = new Fixture();
            fixture.Customize<Order>(order => order.Without(s => s.Id));
            List<Order> orders = fixture.CreateMany<Order>(100).ToList();
            context.AddRange(orders);
        }

        context.SaveChanges();
    }
}