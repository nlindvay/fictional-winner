using Fw.Domain.Common.Dtos;
using Fw.Domain.Tms.Entities;
using Mapster;

namespace Fw.Application.Tms.Mappings;

public class ShipmentMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<OrderDto, Shipment>()
            .Map(d => d.OrderId, s => s.Id);
    }
}