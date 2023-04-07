using AutoMapper;
using Fw.Domain.Ams.Contracts;
using Fw.Domain.Ams.Entities;
using Fw.Domain.Common.Dtos;

namespace Fw.Application.Ams.Mappings;

public class InvoiceMapping : Profile
{
    public InvoiceMapping()
    {
        CreateMap<Invoice, InvoiceDto>()
            .ForMember(dest => dest.InvoiceLines, opt => opt.MapFrom(src => src.InvoiceLines))
            .ReverseMap();

        CreateMap<InvoiceLine, InvoiceLineDto>()
            .ReverseMap();
            
        CreateMap<SubmitInvoice, Invoice>()
            .ReverseMap();

        CreateMap<SubmitInvoiceLine, InvoiceLine>()
            .ReverseMap();

        CreateMap<ShipmentDto, Invoice>()
            .ForMember(d => d.ShipmentId, opt => opt.MapFrom(s => s.Id))
            .ReverseMap();
    }
}