using AutoMapper;
using Fw.Domain.Wms.Contracts;
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

        
    }
}