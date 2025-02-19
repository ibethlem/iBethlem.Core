using iBethlem.Core.Data;
using iBethlem.Core.Tests.Data;
using iBethlem.Core.Tests.Mappers;
using iBethlem.Core.Tests.Models;
using Microsoft.EntityFrameworkCore;

namespace iBethlem.Core.Tests.Repositories;

public class RepositoryAITests
{
    private readonly MockRepository _repository;

    public RepositoryAITests()
    {
        _repository = new MockRepository(new MockMapper(), new MockContext(new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase("MockDatabase").Options));
    }

    [Fact]
    public async Task CreateEntityAsync_ShouldAddEntity()
    {
        var model = new MockModel { Name = "Test", MockProperty = "TestProperty" };
        var result = await _repository.CreateEntityAsync(model);

        Assert.NotNull(result);
        Assert.Equal(model.Name, result.Name);
        Assert.Equal(model.MockProperty, result.MockProperty);
    }

    [Fact]
    public async Task GetEntityAsync_ShouldReturnEntity_WhenEntityExists()
    {
        var model = new MockModel { Name = "Test", MockProperty = "TestProperty" };
        var createdModel = await _repository.CreateEntityAsync(model);

        var result = await _repository.GetEntityAsync(createdModel.Id);

        Assert.NotNull(result);
        Assert.Equal(createdModel.Name, result.Name);
        Assert.Equal(createdModel.MockProperty, result.MockProperty);
    }

    [Fact]
    public async Task GetEntityAsync_ShouldReturnNull_WhenEntityDoesNotExist()
    {
        var result = await _repository.GetEntityAsync(999);

        Assert.Null(result);
    }

    [Fact]
    public async Task UpdateEntityAsync_ShouldUpdateEntity()
    {
        var model = new MockModel { Name = "Test", MockProperty = "TestProperty" };
        var createdModel = await _repository.CreateEntityAsync(model);

        createdModel.Name = "UpdatedTest";
        createdModel.MockProperty = "UpdatedTestProperty";
        var result = await _repository.UpdateEntityAsync(createdModel.Id, createdModel);

        Assert.NotNull(result);
        Assert.Equal("UpdatedTest", result.Name);
        Assert.Equal("UpdatedTestProperty", result.MockProperty);
    }

    [Fact]
    public async Task DeleteEntityAsync_ShouldRemoveEntity()
    {
        var model = new MockModel { Name = "Test", MockProperty = "TestProperty" };
        var createdModel = await _repository.CreateEntityAsync(model);

        var result = await _repository.DeleteEntityAsync(createdModel.Id);

        Assert.NotNull(result);
        Assert.Equal(createdModel.Name, result.Name);
        Assert.Equal(createdModel.MockProperty, result.MockProperty);

        var deletedEntity = await _repository.GetEntityAsync(createdModel.Id);
        Assert.Null(deletedEntity);
    }

    [Fact]
    public void GetEntities_ShouldReturnAllEntities()
    {
        var result = _repository.GetEntities().ToList();

        Assert.NotNull(result);
        Assert.IsType<List<MockModel>>(result);
    }

    [Fact]
    public void GetEntities_WithPredicate_ShouldReturnFilteredEntities()
    {
        var result = _repository.GetEntities(e => e.Name == "Test").ToList();

        Assert.NotNull(result);
        Assert.IsType<List<MockModel>>(result);
    }
}
