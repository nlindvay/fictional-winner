using Fw.Domain.Ams.Entities;
using Fw.Domain.Common.Dtos;
using Mapster;

namespace Fw.Application.Ams.Mappings;

public class InvoiceMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ShipmentDto, Invoice>()
            .Map(d => d.ShipmentId, s => s.Id);
    }
}