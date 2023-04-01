using AutoMapper;
using Fw.Domain.Common.Dtos;
using Fw.Domain.Wms.Entities;

namespace Fw.Application.Wms.Mappings;

public class SkuMappings : Profile

{
    public SkuMappings()
    {
        CreateMap<Sku, SkuDto>()
            .ReverseMap();
    }
}