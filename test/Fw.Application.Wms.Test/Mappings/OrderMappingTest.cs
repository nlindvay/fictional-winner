using System.Reflection;
using AutoFixture;
using FluentAssertions;
using Fw.Application.Wms.Mappings;
using Fw.Application.Common.Mappings;
using Fw.Domain.Wms.Contracts;
using Fw.Domain.Wms.Entities;
using Fw.Domain.Common.Dtos;
using Mapster;
using MapsterMapper;
using Xunit;

namespace Fw.Application.Wms.Test.Mappings;

public class OrderMappingTest
{
    protected readonly IFixture _fixture;
    protected readonly IMapper _mapper;

    public OrderMappingTest()
    {
        _fixture = new Fixture();
        _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => _fixture.Behaviors.Remove(b));
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetAssembly(typeof(OrderMapping)));
        config.Scan(Assembly.GetAssembly(typeof(IEntityMapping)));
        _mapper = new Mapper(config);
    }
}