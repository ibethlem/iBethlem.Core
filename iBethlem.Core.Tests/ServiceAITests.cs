using iBethlem.Core.Data;
using iBethlem.Core.Tests.Data;
using iBethlem.Core.Tests.Mappers;
using iBethlem.Core.Tests.Models;
using iBethlem.Core.Tests.Repositories;
using iBethlem.Core.Tests.Services;
using Microsoft.EntityFrameworkCore;

namespace iBethlem.Core.Tests;

public class ServiceAITests
{
    private readonly MockService _service;

    public ServiceAITests()
    {
        var repository = new MockRepository(new MockMapper(), new MockContext(new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase("MockDatabase").Options));
        _service = new MockService(repository);
    }

    [Fact]
    public async Task CreateAsync_ShouldReturnError_WhenIdIsNotEmpty()
    {
        var model = new MockModel { Id = 1, Name = "Test", MockProperty = "TestProperty" };
        var result = await _service.CreateAsync(model).ConfigureAwait(true);
        Assert.True(result.Error);
        Assert.Equal("Id must be empty.", result.Message);
    }

    [Fact]
    public async Task CreateAsync_ShouldReturnError_WhenNameIsEmpty()
    {
        var model = new MockModel { Id = 0, Name = "", MockProperty = "TestProperty" };
        var result = await _service.CreateAsync(model).ConfigureAwait(true);
        Assert.True(result.Error);
        Assert.Equal("Name must be informed.", result.Message);
    }

    [Fact]
    public async Task CreateAsync_ShouldReturnError_WhenMockPropertyIsEmpty()
    {
        var model = new MockModel { Id = 0, Name = "Test", MockProperty = "" };
        var result = await _service.CreateAsync(model).ConfigureAwait(true);
        Assert.True(result.Error);
        Assert.Equal("MockProperty must be informed.", result.Message);
    }

    [Fact]
    public async Task UpdateAsync_ShouldReturnError_WhenIdIsZero()
    {
        var model = new MockModel { Id = 0, Name = "Test", MockProperty = "TestProperty" };
        var result = await _service.UpdateAsync(0, model).ConfigureAwait(true);
        Assert.True(result.Error);
        Assert.Equal("Id must be informed.", result.Message);
    }

    [Fact]
    public async Task UpdateAsync_ShouldReturnError_WhenNameIsEmpty()
    {
        var model = new MockModel { Id = 1, Name = "", MockProperty = "TestProperty" };
        var result = await _service.UpdateAsync(1, model).ConfigureAwait(true);
        Assert.True(result.Error);
        Assert.Equal("Name must be informed.", result.Message);
    }

    [Fact]
    public async Task UpdateAsync_ShouldReturnError_WhenMockPropertyIsEmpty()
    {
        var model = new MockModel { Id = 1, Name = "Test", MockProperty = "" };
        var result = await _service.UpdateAsync(1, model).ConfigureAwait(true);
        Assert.True(result.Error);
        Assert.Equal("MockProperty must be informed.", result.Message);
    }

    [Fact]
    public async Task DeleteAsync_ShouldReturnError_WhenIdIsZero()
    {
        var result = await _service.DeleteAsync(0).ConfigureAwait(true);
        Assert.True(result.Error);
        Assert.Equal("Id must be informed.", result.Message);
    }

    [Fact]
    public async Task GetAsync_ShouldReturnError_WhenIdIsZero()
    {
        var result = await _service.GetAsync(0).ConfigureAwait(true);
        Assert.True(result.Error);
        Assert.Equal("Id must be informed.", result.Message);
    }

    [Fact]
    public async Task GetByNameAsync_ShouldReturnError_WhenNameIsEmpty()
    {
        var result = await _service.GetByNameAsync("").ConfigureAwait(true);
        Assert.True(result.Error);
        Assert.Equal("Name must be informed.", result.Message);
    }

    [Fact]
    public async Task GetByMockPropertyAsync_ShouldReturnError_WhenMockPropertyIsEmpty()
    {
        var result = await _service.GetByMockPropertyAsync("").ConfigureAwait(true);
        Assert.True(result.Error);
        Assert.Equal("MockProperty must be informed.", result.Message);
    }

    [Fact]
    public async Task GetByMockPropertyAndNameAsync_ShouldReturnError_WhenMockPropertyIsEmpty()
    {
        var result = await _service.GetByMockPropertyAndNameAsync("", "Test").ConfigureAwait(true);
        Assert.True(result.Error);
        Assert.Equal("MockProperty must be informed.", result.Message);
    }

    [Fact]
    public async Task GetByMockPropertyAndNameAsync_ShouldReturnError_WhenNameIsEmpty()
    {
        var result = await _service.GetByMockPropertyAndNameAsync("TestProperty", "").ConfigureAwait(true);
        Assert.True(result.Error);
        Assert.Equal("Name must be informed.", result.Message);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnEntities()
    {
        var result = await _service.GetAllAsync().ConfigureAwait(true);
        Assert.NotNull(result);
    }
}