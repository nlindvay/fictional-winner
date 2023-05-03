using System.Reflection;
using AutoFixture;
using FluentAssertions;
using Fw.Application.Tms.Mappings;
using Fw.Application.Common.Mappings;
using Fw.Domain.Tms.Contracts;
using Fw.Domain.Tms.Entities;
using Fw.Domain.Common.Dtos;
using Mapster;
using MapsterMapper;
using Xunit;

namespace Fw.Application.Tms.Test.Mappings;

public class ShipmentMappingTest
{
    protected readonly IFixture _fixture;
    protected readonly IMapper _mapper;

    public ShipmentMappingTest()
    {
        _fixture = new Fixture();
        _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => _fixture.Behaviors.Remove(b));
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetAssembly(typeof(ShipmentMapping)));
        config.Scan(Assembly.GetAssembly(typeof(IEntityMapping)));
        _mapper = new Mapper(config);
    }

    // [Fact]
    // public void SubmitInvoiceLine2InvoiceLine()
    // {
    //     var source = _fixture.Create<SubmitInvoiceLine>();
    //     var result = _mapper.Map<InvoiceLine>(source);
    //     result.InvoiceId.Should().Be(source.InvoiceId);
    //     result.LineQuantity.Should().Be(source.LineQuantity);
    //     result.LineNumber.Should().Be(source.LineNumber);
    //     result.LineDescription.Should().Be(source.LineDescription);
    //     result.Id.Should().NotBeEmpty();
    //     result.IsActive.Should().BeTrue();
    //     result.IsDeleted.Should().BeFalse();
    //     result.Version.Should().Be(1);
    // }

    // [Fact]
    // public void SubmitInvoice2Invoice()
    // {
    //     var source = _fixture.Create<SubmitInvoice>();
    //     var result = _mapper.Map<Invoice>(source);
    //     result.PrimaryReference.Should().Be(source.PrimaryReference);
    //     result.SecondaryReference.Should().Be(source.SecondaryReference);
    //     result.Id.Should().NotBeEmpty();
    //     result.IsActive.Should().BeTrue();
    //     result.IsDeleted.Should().BeFalse();
    //     result.Version.Should().Be(1);
    // }

    // [Fact]
    // public void Invoice2InvoiceDto()
    // {
    //     var source = _fixture.Create<Invoice>();
    //     var result = _mapper.Map<InvoiceDto>(source);
    //     result.Id.Should().Be(source.Id);
    //     result.IsActive.Should().Be(source.IsActive);
    //     result.IsDeleted.Should().Be(source.IsDeleted);
    //     result.Version.Should().Be(source.Version);
    //     result.CreatedBy.Should().Be(source.CreatedBy);
    //     result.CreatedDate.Should().Be(source.CreatedDate);
    //     result.LastModifiedBy.Should().Be(source.LastModifiedBy);
    //     result.LastModifiedDate.Should().Be(source.LastModifiedDate);
    //     result.CustomerId.Should().Be(source.CustomerId);
    //     result.PrimaryReference.Should().Be(source.PrimaryReference);
    //     result.SecondaryReference.Should().Be(source.SecondaryReference);
    //     result.InvoiceStatus.Should().Be(source.InvoiceStatus);
    //     result.InvoiceLines.Should().BeAssignableTo(typeof(InvoiceLineDto[]));
    //     result.InvoiceLines.Count().Should().Be(source.InvoiceLines.Count());
    //     result.OrderId.Should().Be(source.OrderId);
    //     result.OrderStatus.Should().Be(source.OrderStatus);
    //     result.ShipmentId.Should().Be(source.ShipmentId);
    //     result.ShipmentStatus.Should().Be(source.ShipmentStatus);
    // }

    // [Fact]
    // public void InvoiceLine2InvoiceLineDto()
    // {
    //     var source = _fixture.Create<InvoiceLine>();
    //     var result = _mapper.Map<InvoiceLineDto>(source);
    //     result.Id.Should().Be(source.Id);
    //     result.IsActive.Should().Be(source.IsActive);
    //     result.IsDeleted.Should().Be(source.IsDeleted);
    //     result.Version.Should().Be(source.Version);
    //     result.CreatedBy.Should().Be(source.CreatedBy);
    //     result.CreatedDate.Should().Be(source.CreatedDate);
    //     result.LastModifiedBy.Should().Be(source.LastModifiedBy);
    //     result.LastModifiedDate.Should().Be(source.LastModifiedDate);
    //     result.CustomerId.Should().Be(source.CustomerId);
    //     result.LineNumber.Should().Be(source.LineNumber);
    //     result.LineDescription.Should().Be(source.LineDescription);
    //     result.LineQuantity.Should().Be(source.LineQuantity);
    //     result.LineCost.Should().Be(source.LineCost);
    //     result.InvoiceId.Should().Be(source.InvoiceId);
    // }

    // [Fact]
    // public void ShipmentDto2Invoice()
    // {
    //     ;
    //     var source = _fixture.Create<ShipmentDto>();
    //     var result = _mapper.Map<Invoice>(source);
    //     result.ShipmentId.Should().Be(source.Id);
    // }
}