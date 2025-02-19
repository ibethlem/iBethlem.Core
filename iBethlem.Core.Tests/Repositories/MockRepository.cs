using iBethlem.Core.Implementations.Repositories;
using iBethlem.Core.Tests.Data;
using iBethlem.Core.Tests.Mappers;
using iBethlem.Core.Tests.Models;

namespace iBethlem.Core.Tests.Repositories;

public class MockRepository : BaseRepository<MockEntity, MockModel>
{
    public MockRepository(MockMapper mapper, MockContext context) : base(mapper, context)
    {
        context.Database.EnsureCreated();
    }
}
