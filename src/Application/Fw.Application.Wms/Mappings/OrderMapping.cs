using AutoMapper;
using Fw.Domain.Common.Contracts;
using Fw.Domain.Common.Dtos;
using Fw.Domain.Wms.Entities;

namespace Fw.Application.Wms.Mappings;

public class OrderMappings : Profile

{
    public OrderMappings()
    {
        CreateMap<Order, OrderDto>()
            .ForMember(dest => dest.OrderLines, opt => opt.MapFrom(src => src.OrderLines))
            .ReverseMap();
        CreateMap<OrderLine, OrderLineDto>()
            .ReverseMap();
        CreateMap<Order, ShipOrder>()
            .ForMember(dest => dest.Order, opt => opt.MapFrom(src => src))
            .ReverseMap();
        
    }
}