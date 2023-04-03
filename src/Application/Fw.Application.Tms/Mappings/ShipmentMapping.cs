using AutoMapper;
using Fw.Domain.Tms.Contracts;
using Fw.Domain.Common.Dtos;
using Fw.Domain.Tms.Entities;
using Fw.Domain.Common.Contracts;

namespace Fw.Application.Tms.Mappings;

public class ShipmentMapping : Profile
{
    public ShipmentMapping()
    {
        CreateMap<Shipment, ShipmentDto>()
            .ForMember(dest => dest.Packages, opt => opt.MapFrom(src => src.Packages))
            .ReverseMap();
        CreateMap<Pack, PackDto>()
            .ForMember(dest => dest.PackLines, opt => opt.MapFrom(src => src.PackLines))
            .ReverseMap();
        CreateMap<PackLine, PackLineDto>()
            .ReverseMap();

        CreateMap<SubmitShipment, Shipment>()
            .ReverseMap();

        CreateMap<SubmitPack, Pack>()
            .ReverseMap();

        CreateMap<SubmitPackLine, PackLine>()
            .ReverseMap();

        CreateMap<OrderDto, Shipment>();

        

    }
}