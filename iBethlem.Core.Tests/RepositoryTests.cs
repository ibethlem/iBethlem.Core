using iBethlem.Core.Data;
using iBethlem.Core.Tests.Data;
using iBethlem.Core.Tests.Mappers;
using iBethlem.Core.Tests.Models;
using iBethlem.Core.Tests.Repositories;
using Microsoft.EntityFrameworkCore;

namespace iBethlem.Core.Tests;

public class RepositoryTests
{
    private readonly MockRepository _repository;

    public RepositoryTests()
    {
        _repository = new MockRepository(new MockMapper(), new MockContext(new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase("MockDatabase").Options));
    }

    [Fact]
    public async Task GetEntityTest()
    {
        // arrange
        var entityId = 1;

        // act
        var entity = await _repository.GetEntityAsync(entityId).ConfigureAwait(true);

        // assert
        Assert.NotNull(entity);
        Assert.Equal(entityId, entity.Id);
    }

    [Fact]
    public void GetEntitiesTest()
    {
        // act
        var entities = _repository.GetEntities();

        // assert
        Assert.NotNull(entities);
        Assert.NotEmpty(entities);
    }

    [Fact]
    public async Task CreateAndDeleteEntityTest()
    {
        // arrange
        var entity = new MockModel { Name = "MockEntity2", MockProperty = "MockProperty2" };

        // act
        var createdEntity = await _repository.CreateEntityAsync(entity).ConfigureAwait(true);

        // assert
        Assert.NotNull(createdEntity);
        Assert.True(createdEntity.Id > 0);
        Assert.Equal(entity.Name, createdEntity.Name);
        Assert.Equal(entity.MockProperty, createdEntity.MockProperty);

        // arrange
        var entityId = createdEntity.Id;

        // act
        var deletedEntity = await _repository.DeleteEntityAsync(entityId).ConfigureAwait(true);

        // assert
        Assert.NotNull(deletedEntity);
        Assert.Equal(entityId, deletedEntity.Id);

        // act
        deletedEntity = await _repository.GetEntityAsync(entityId).ConfigureAwait(true);

        // assert
        Assert.Null(deletedEntity);
    }

    [Fact]
    public async Task UpdateAndRevertEntityTest()
    {
        // arrange
        var entityId = 1;
        var entity = new MockModel { Id = entityId, Name = "MockEntity2", MockProperty = "MockProperty2" };

        // act
        var updatedEntity = await _repository.UpdateEntityAsync(entityId, entity).ConfigureAwait(true);

        // assert
        Assert.NotNull(updatedEntity);
        Assert.Equal(entityId, updatedEntity.Id);
        Assert.Equal(entity.Name, updatedEntity.Name);
        Assert.Equal(entity.MockProperty, updatedEntity.MockProperty);

        // act
        updatedEntity = await _repository.GetEntityAsync(entityId).ConfigureAwait(true);

        // assert
        Assert.NotNull(updatedEntity);
        Assert.Equal(entityId, updatedEntity.Id);
        Assert.Equal(entity.Name, updatedEntity.Name);
        Assert.Equal(entity.MockProperty, updatedEntity.MockProperty);

        // arrange
        entity = new MockModel { Id = entityId, Name = "MockEntity1", MockProperty = "MockProperty1" };

        // act
        var revertedEntity = await _repository.UpdateEntityAsync(entityId, entity).ConfigureAwait(true);

        // assert
        Assert.NotNull(revertedEntity);
        Assert.Equal(entityId, revertedEntity.Id);
        Assert.Equal(entity.Name, revertedEntity.Name);
        Assert.Equal(entity.MockProperty, revertedEntity.MockProperty);

        // act
        revertedEntity = await _repository.GetEntityAsync(entityId).ConfigureAwait(true);

        // assert
        Assert.NotNull(revertedEntity);
        Assert.Equal(entityId, revertedEntity.Id);
        Assert.Equal(entity.Name, revertedEntity.Name);
        Assert.Equal(entity.MockProperty, revertedEntity.MockProperty);
    }

    [Fact]
    public async Task GetByNameTest()
    {
        // arrange
        var entityName = "MockEntity1";

        // act
        var entity = await _repository.GetEntities(entity => entity.Name == entityName).FirstOrDefaultAsync().ConfigureAwait(true);

        // assert
        Assert.NotNull(entity);
        Assert.Equal(entityName, entity.Name);
    }

    [Fact]
    public async Task GetByNameNotFoundTest()
    {
        // arrange
        var entityName = "MockEntity3";
        // act
        var entity = await _repository.GetEntities(entity => entity.Name == entityName).FirstOrDefaultAsync().ConfigureAwait(true);
        // assert
        Assert.Null(entity);
    }

    [Fact]
    public async Task GetByNameEmptyTest()
    {
        // arrange
        var entityName = string.Empty;

        // act
        var entity = await _repository.GetEntities(entity => entity.Name == entityName).FirstOrDefaultAsync().ConfigureAwait(true);

        // assert
        Assert.Null(entity);
    }

    [Fact]
    public async Task GetByNameNullTest()
    {
        // arrange
        string entityName = null;

        // act
        var entity = await _repository.GetEntities(entity => entity.Name == entityName).FirstOrDefaultAsync().ConfigureAwait(true);

        // assert
        Assert.Null(entity);
    }

    [Fact]
    public async Task GetByNameWhiteSpaceTest()
    {
        // arrange
        var entityName = " ";

        // act
        var entity = await _repository.GetEntities(entity => entity.Name == entityName).FirstOrDefaultAsync().ConfigureAwait(true);

        // assert
        Assert.Null(entity);
    }

    [Fact]
    public async Task GetByMockPropertyTest()
    {
        // arrange
        var entityMockProperty = "MockProperty1";

        // act
        var entity = await _repository.GetEntities(entity => entity.MockProperty == entityMockProperty).FirstOrDefaultAsync().ConfigureAwait(true);

        // assert
        Assert.NotNull(entity);
        Assert.Equal(entityMockProperty, entity.MockProperty);
    }

    [Fact]
    public async Task GetByMockPropertyNotFoundTest()
    {
        // arrange
        var entityMockProperty = "MockProperty3";

        // act
        var entity = await _repository.GetEntities(entity => entity.MockProperty == entityMockProperty).FirstOrDefaultAsync().ConfigureAwait(true);

        // assert
        Assert.Null(entity);
    }

    [Fact]
    public async Task GetByMockPropertyEmptyTest()
    {
        // arrange
        var entityMockProperty = string.Empty;

        // act
        var entity = await _repository.GetEntities(entity => entity.MockProperty == entityMockProperty).FirstOrDefaultAsync().ConfigureAwait(true);

        // assert
        Assert.Null(entity);
    }

    [Fact]
    public async Task GetByMockPropertyNullTest()
    {
        // arrange
        string entityMockProperty = null;

        // act
        var entity = await _repository.GetEntities(entity => entity.MockProperty == entityMockProperty).FirstOrDefaultAsync().ConfigureAwait(true);

        // assert
        Assert.Null(entity);
    }

    [Fact]
    public async Task GetByMockPropertyWhiteSpaceTest()
    {
        // arrange
        var entityMockProperty = " ";
        // act
        var entity = await _repository.GetEntities(entity => entity.MockProperty == entityMockProperty).FirstOrDefaultAsync().ConfigureAwait(true);
        // assert
        Assert.Null(entity);
    }

    [Fact]
    public async Task GetByNameAndMockPropertyTest()
    {
        // arrange
        var entityName = "MockEntity1";
        var entityMockProperty = "MockProperty1";

        // act
        var entity = await _repository.GetEntities(entity => entity.Name == entityName && entity.MockProperty == entityMockProperty).FirstOrDefaultAsync().ConfigureAwait(true);

        // assert
        Assert.NotNull(entity);
        Assert.Equal(entityName, entity.Name);
        Assert.Equal(entityMockProperty, entity.MockProperty);
    }

    [Fact]
    public async Task GetByNameAndMockPropertyNotFoundTest()
    {
        // arrange
        var entityName = "MockEntity3";
        var entityMockProperty = "MockProperty3";

        // act
        var entity = await _repository.GetEntities(entity => entity.Name == entityName && entity.MockProperty == entityMockProperty).FirstOrDefaultAsync().ConfigureAwait(true);

        // assert
        Assert.Null(entity);
    }

    [Fact]
    public async Task GetByNameAndMockPropertyEmptyTest()
    {
        // arrange
        var entityName = string.Empty;
        var entityMockProperty = string.Empty;

        // act
        var entity = await _repository.GetEntities(entity => entity.Name == entityName && entity.MockProperty == entityMockProperty).FirstOrDefaultAsync().ConfigureAwait(true);

        // assert
        Assert.Null(entity);
    }

    [Fact]
    public async Task GetByNameAndMockPropertyNullTest()
    {
        // arrange
        string entityName = null;
        string entityMockProperty = null;

        // act
        var entity = await _repository.GetEntities(entity => entity.Name == entityName && entity.MockProperty == entityMockProperty).FirstOrDefaultAsync().ConfigureAwait(true);

        // assert
        Assert.Null(entity);
    }

    [Fact]
    public async Task GetByNameAndMockPropertyWhiteSpaceTest()
    {
        // arrange
        var entityName = " ";
        var entityMockProperty = " ";

        // act
        var entity = await _repository.GetEntities(entity => entity.Name == entityName && entity.MockProperty == entityMockProperty).FirstOrDefaultAsync().ConfigureAwait(true);

        // assert
        Assert.Null(entity);
    }
}
