using Fw.Domain.Common.Interfaces;
using Mapster;
using MassTransit;

namespace Fw.Application.Common.Mappings;

public class IEntityMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForDestinationType<IEntity>()
            .IgnoreNullValues(true)
            .AfterMapping((dest) =>
            {
                if (dest.Id == Guid.Empty)
                {
                    dest.Id = NewId.NextGuid();
                    dest.IsActive = true;
                    dest.IsDeleted = false;
                    dest.Version = 1;
                }
            });
    }
}