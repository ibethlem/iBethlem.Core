using iBethlem.Core.Abstractions.Mappers;
using iBethlem.Core.Tests.Models;

namespace iBethlem.Core.Tests.Mappers;

public class MockMapper : IModelMapper<MockEntity, MockModel>
{
    public MockEntity MapToEntity(MockModel model)
    {
        return new MockEntity
        {
            Id = model.Id,
            Name = model.Name,
            MockProperty = model.MockProperty
        };
    }

    public MockModel MapToModel(MockEntity entity)
    {
        return new MockModel
        {
            Id = entity.Id,
            Name = entity.Name,
            MockProperty = entity.MockProperty
        };
    }
}
