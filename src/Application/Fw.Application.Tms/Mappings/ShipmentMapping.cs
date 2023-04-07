using AutoMapper;
using Fw.Domain.Tms.Contracts;
using Fw.Domain.Common.Dtos;
using Fw.Domain.Tms.Entities;
using Fw.Domain.Common.Contracts;
using MassTransit;

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

        CreateMap<OrderDto, Shipment>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => NewId.NextGuid()))
            .ForMember(d => d.OrderId, opt => opt.MapFrom(s => s.Id))
            .ReverseMap();

        CreateMap<OrderLineDto, Pack>();

        CreateMap<Shipment, InvoiceShipment>()
            .ForMember(dest => dest.Shipment, opt => opt.MapFrom(src => src))
            .ReverseMap();
    }
}