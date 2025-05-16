using iBethlem.Core.Data;
using iBethlem.Core.Tests.Data;
using iBethlem.Core.Tests.Mappers;
using iBethlem.Core.Tests.Models;
using iBethlem.Core.Tests.Repositories;
using iBethlem.Core.Tests.Services;
using Microsoft.EntityFrameworkCore;

namespace iBethlem.Core.Tests;

public class ServiceTests
{
    private readonly MockService _service;
    public ServiceTests()
    {
        var repository = new MockRepository(new MockMapper(), new MockContext(new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase("MockDatabase").Options));
        _service = new MockService(repository);
    }

    [Fact]
    public async Task GetModelTest()
    {
        // arrange
        var modelId = 1;

        // act
        var response = await _service.GetAsync(modelId).ConfigureAwait(true);

        // assert
        Assert.NotNull(response);
        Assert.False(response.Error);
        Assert.Null(response.Message);
        Assert.Equal(modelId, response.Data.Id);
    }

    [Fact]
    public async Task GetModelsTest()
    {
        // act
        var response = await _service.GetAllAsync().ConfigureAwait(true);

        // assert
        Assert.NotNull(response);
        var item = response.First();
        Assert.True(item.Id > 0);
    }

    [Fact]
    public async Task CreateAndDeleteModelTest()
    {
        // arrange
        var model = new MockModel { Name = "MockModel2", MockProperty = "MockProperty2" };

        // act
        var response = await _service.CreateAsync(model).ConfigureAwait(true);

        // assert
        Assert.NotNull(response);
        Assert.Null(response.Message);
        Assert.False(response.Error);
        Assert.True(response.Data.Id > 0);
        Assert.Equal(model.Name, response.Data.Name);
        Assert.Equal(model.MockProperty, response.Data.MockProperty);

        // act
        response = await _service.DeleteAsync(response.Data.Id).ConfigureAwait(true);

        // assert
        Assert.NotNull(response);
        Assert.False(response.Error);
        Assert.Null(response.Message);
        Assert.True(response.Data.Id > 0);
        Assert.Equal(model.Name, response.Data.Name);
        Assert.Equal(model.MockProperty, response.Data.MockProperty);

        // act
        response = await _service.GetAsync(response.Data.Id).ConfigureAwait(true);
        Assert.NotNull(response);
        Assert.True(response.Error);
        Assert.NotEmpty(response.Message);
        Assert.Contains("not found", response.Message);
    }

    [Fact]
    public async Task UpdateAndRevertModelTest()
    {
        // arrange
        var modelId = 1;
        var model = new MockModel { Id = modelId, Name = "MockModel2", MockProperty = "MockProperty2" };

        // act
        var response = await _service.UpdateAsync(modelId, model).ConfigureAwait(true);

        // assert
        Assert.NotNull(response);
        Assert.False(response.Error);
        Assert.Null(response.Message);
        Assert.Equal(modelId, response.Data.Id);
        Assert.Equal(model.Name, response.Data.Name);
        Assert.Equal(model.MockProperty, response.Data.MockProperty);

        // arrange
        model = new MockModel { Id = modelId, Name = "MockModel1", MockProperty = "MockProperty1" };

        // act
        response = await _service.UpdateAsync(modelId, model).ConfigureAwait(true);

        // assert
        Assert.NotNull(response);
        Assert.False(response.Error);
        Assert.Null(response.Message);
        Assert.Equal(modelId, response.Data.Id);
        Assert.Equal(model.Name, response.Data.Name);
        Assert.Equal(model.MockProperty, response.Data.MockProperty);

        // act
        response = await _service.GetAsync(modelId).ConfigureAwait(true);

        // assert
        Assert.NotNull(response);
        Assert.False(response.Error);
        Assert.Null(response.Message);
        Assert.Equal(modelId, response.Data.Id);
        Assert.Equal(model.Name, response.Data.Name);
        Assert.Equal(model.MockProperty, response.Data.MockProperty);
    }

    [Fact]
    public async Task CreateModelWithInvalidNameTest()
    {
        // arrange
        var model = new MockModel { Name = "", MockProperty = "MockProperty2" };

        // act
        var response = await _service.CreateAsync(model).ConfigureAwait(true);

        // assert
        Assert.NotNull(response);
        Assert.True(response.Error);
        Assert.NotEmpty(response.Message);
        Assert.Contains("Name", response.Message);
        Assert.Contains("informed", response.Message);
    }

    [Fact]
    public async Task CreateModelWithInvalidMockPropertyTest()
    {
        // arrange
        var model = new MockModel { Name = "MockModel2", MockProperty = "" };

        // act
        var response = await _service.CreateAsync(model).ConfigureAwait(true);

        // assert
        Assert.NotNull(response);
        Assert.True(response.Error);
        Assert.NotEmpty(response.Message);
        Assert.Contains("MockProperty", response.Message);
        Assert.Contains("informed", response.Message);
    }

    [Fact]
    public async Task UpdateModelWithInvalidIdTest()
    {
        // arrange
        var modelId = 0;
        var model = new MockModel { Id = modelId, Name = "MockModel2", MockProperty = "MockProperty2" };

        // act
        var response = await _service.UpdateAsync(modelId, model).ConfigureAwait(true);

        // assert
        Assert.NotNull(response);
        Assert.True(response.Error);
        Assert.NotEmpty(response.Message);
        Assert.Contains("Id", response.Message);
        Assert.Contains("informed", response.Message);
    }

    [Fact]
    public async Task UpdateModelWithInvalidNameTest()
    {
        // arrange
        var modelId = 1;
        var model = new MockModel { Id = modelId, Name = "", MockProperty = "MockProperty2" };

        // act
        var response = await _service.UpdateAsync(modelId, model).ConfigureAwait(true);

        // assert
        Assert.NotNull(response);
        Assert.True(response.Error);
        Assert.NotEmpty(response.Message);
        Assert.Contains("Name", response.Message);
        Assert.Contains("informed", response.Message);
    }

    [Fact]
    public async Task UpdateModelWithInvalidMockPropertyTest()
    {
        // arrange
        var modelId = 1;
        var model = new MockModel { Id = modelId, Name = "MockModel2", MockProperty = "" };

        // act
        var response = await _service.UpdateAsync(modelId, model).ConfigureAwait(true);

        // assert
        Assert.NotNull(response);
        Assert.True(response.Error);
        Assert.NotEmpty(response.Message);
        Assert.Contains("MockProperty", response.Message);
        Assert.Contains("informed", response.Message);
    }

    [Fact]
    public async Task DeleteModelWithInvalidIdTest()
    {
        // arrange
        var modelId = 0;

        // act
        var response = await _service.DeleteAsync(modelId).ConfigureAwait(true);

        // assert
        Assert.NotNull(response);
        Assert.True(response.Error);
        Assert.NotEmpty(response.Message);
        Assert.Contains("Id", response.Message);
        Assert.Contains("informed", response.Message);
    }

    [Fact]
    public async Task GetModelWithInvalidIdTest()
    {
        // arrange
        var modelId = 0;

        // act
        var response = await _service.GetAsync(modelId).ConfigureAwait(true);

        // assert
        Assert.NotNull(response);
        Assert.True(response.Error);
        Assert.NotEmpty(response.Message);
        Assert.Contains("Id", response.Message);
        Assert.Contains("informed", response.Message);
    }
}
