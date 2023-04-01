using AutoMapper;
using Fw.Domain.Wms.Contracts;
using Fw.Domain.Wms.Entities;

namespace Fw.Application.Wms.Mappings;

public class ReceiveMappings : Profile

{
    public ReceiveMappings()
    {
        CreateMap<Receive, ReceiveDto>()
            .ForMember(dest => dest.ReceiveLines, opt => opt.MapFrom(src => src.ReceiveLines))
            .ReverseMap();
        CreateMap<ReceiveLine, ReceiveLineDto>()
            .ReverseMap();
    }
}